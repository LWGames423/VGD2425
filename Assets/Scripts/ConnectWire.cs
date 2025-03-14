using UnityEngine;

public class ConnectWire : MonoBehaviour
{
    public DetectWire wireStart, wireEnd;
    public bool connected = false;
    public PowerManager pm;
    public GameObject end;

    private int power;
    private bool triggered = false;

    private void Awake()
    {
        if (!wireEnd)
        {
            end.GetComponent<PowerManager>().power += pm.power;
        }

        power = pm.power;
    }

    private void Update()
    {
        if (wireStart && wireEnd)
        {
            connected = wireStart.connected && wireEnd.connected;
            if (connected)
            {
                if (end != null && !triggered && pm.power > 0)
                {
                    end.GetComponent<PowerManager>().power += pm.power;
                    triggered = true;
                }
            }
            else
            {
                if (end != null && triggered && pm.power > 0)
                {
                    end.GetComponent<PowerManager>().power -= pm.power;
                    triggered = false;
                }
            }
        }

        else if (power != pm.power)
        {
            end.GetComponent<PowerManager>().power = pm.power;
            power = pm.power;
        }
        else if (wireStart.interrupted)
        {
            pm.power = 0;
        }
    }
}
