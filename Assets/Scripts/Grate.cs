using UnityEngine;

public class Grate : MonoBehaviour
{
    public Kill killScript;
    public PowerManager power;
    public PlayerManager pm;
    public BoxCollider2D col;

    private void Update()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        GetComponent<Animator>().SetInteger("power", power.power);

        if (power.power > 0)
        {
            killScript.enabled = true;
        }
        else
        {
            killScript.enabled = false;
        }

        if (pm.currentCharacter == 0 || pm.currentCharacter == 2 || pm.currentCharacter == 4 || pm.currentCharacter == 6)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (power.power == 0 && collision.CompareTag("Player") && pm.currentCharacter == 2)
        {
            GetComponent<Animator>().SetBool("melt", true);
            col.enabled = false;
        }
    }

    public void Suicide()
    {
        Destroy(gameObject);
    }
}
