using UnityEngine;
using System.Collections.Generic;

public class Flamethrower : MonoBehaviour
{
    public float onInterval, offInterval;
    private float interval = 0, timer;
    private bool status = false, blocked = false;
    public Animator anim;
    public PowerManager pm;
    private PlayerManager player;
    private int currentCharacter;
    public List<int> killable;

    private SteelPlatform steel;
    private Kill killScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        killScript = GetComponent<Kill>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && currentCharacter == 2)
        {
            player.GetComponent<Abilities>().BecomeCharcoal();  
        }
        if (collision.CompareTag("PlayerPlatform"))
        {
            steel = collision.GetComponent<SteelPlatform>();
        }
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        killScript.enabled = status;
        if (steel)
        {
            steel.heat++;

        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        currentCharacter = player.currentCharacter;

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
                interval = timer + offInterval;
                anim.SetBool("Ignite", false);
                status = false;
            }
        }
    }
}
