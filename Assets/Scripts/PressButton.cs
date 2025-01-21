using UnityEngine;

public class PressButton : MonoBehaviour
{
    public bool holdButton = false;
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerPlatform"))
        {
            door.GetComponent<Animator>().SetBool("Open", true);
            door.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (holdButton)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("PlayerPlatform"))
            {
                door.GetComponent<Animator>().SetBool("Open", false);
                door.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
