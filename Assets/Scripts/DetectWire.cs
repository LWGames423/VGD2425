using UnityEngine;

public class DetectWire : MonoBehaviour
{
    public bool connected;
    private int connections;

    private PlayerManager pm;

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (!pm)
        {
            pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") && pm.currentCharacter == 0) || collision.CompareTag("PlayerPlatform"))
        {
            connected = true;
            connections++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") && pm.currentCharacter == 0) || collision.CompareTag("PlayerPlatform"))
        {
            connected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") && pm.currentCharacter == 0) || collision.CompareTag("PlayerPlatform"))
        {
            connections--;
            if (connections <= 0)
            {
                connected = false;
            }
        }
    }
}
