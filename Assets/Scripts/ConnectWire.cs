using UnityEngine;

public class ConnectWire : MonoBehaviour
{
    public DetectWire wireStart, wireEnd;
    public GameObject door;
    public GameObject flame;

    private void Update()
    {
        if (wireStart.connected && wireEnd.connected)
        {
            if (door != null)
            {
                door.GetComponent<Animator>().SetBool("Open", true);
                door.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            if (door != null)
            {
                door.GetComponent<Animator>().SetBool("Open", false);
                door.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
