using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public float onInterval, offInterval;
    private float interval = 0, timer;
    private bool status = false, blocked = false;
    public Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
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
}
