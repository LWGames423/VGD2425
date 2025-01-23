using UnityEngine;

public class SteamEngineManager : MonoBehaviour
{
    public PowerManager pm;
    public PlayerManager player;
    private bool fuel = false, water = false, lit = false;
    public SpriteRenderer jug;
    public Sprite empty, full;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        jug.sprite = empty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.currentCharacter == 3 && collision.CompareTag("Player"))
        {
            if (fuel && water)
            {
                lit = true;
            }
        }
        else if (player.currentCharacter == 1 && collision.CompareTag("Player"))
        {
            water = true;
            jug.sprite = full;
        }
        else if (player.currentCharacter == 4 && collision.CompareTag("Player"))
        {
            fuel = true;
        }
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("fuel", lit);
        GetComponent<Animator>().SetBool("water", fuel);
        if (fuel && lit && water)
        {
            pm.power = 1;
        }
    }
}
