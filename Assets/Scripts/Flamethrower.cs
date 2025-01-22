using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public float onInterval, offInterval;
    private float interval = 0, timer;
    private bool status = false, blocked = false;
    public Animator anim;
    public PowerManager pm;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (pm.power == -1)
        {
            if (status && interval + offInterval <= timer)
            {
                interval = timer + offInterval;
                anim.SetBool("Ignite", false);
                status = false;
            }
            else if (!status && interval + onInterval <= timer)
            {
                interval = timer + onInterval;
                anim.SetBool("Ignite", true);
                status = true;
            }
        }
        else
        {
            if (pm.power > 0)
            {
                interval = timer + onInterval;
                anim.SetBool("Ignite", true);
                status = true;
                
            }
            else
            {
                interval = timer + offInterval;
                anim.SetBool("Ignite", false);
                status = false;
            }
        }
    }
}
