using UnityEngine;

public class SteelPlatform : MonoBehaviour
{
    public PlayerManager pm;
    public int heat = 0;

    void Awake()
    {
        heat = 0;
    }

    private void Update()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        if (heat > 5000)
        {
            GetComponent<Animator>().SetBool("melt", true);
        }

        GetComponent<SpriteRenderer>().color = new Color(1 + heat / 300, 1 + heat / 500, 1);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") && pm.currentCharacter == 1))
        {
            GetComponent<Animator>().SetBool("rust", true);
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") && pm.currentCharacter == 3))
        {
            heat += 10;
        }
    }
}
