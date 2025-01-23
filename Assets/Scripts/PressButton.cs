using UnityEngine;

public class PressButton : MonoBehaviour
{
    public bool holdButton = false;
    public SpriteRenderer spriteRenderer;
    public Sprite on, off;
    public GameObject wire;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerPlatform"))
        {
            wire.GetComponent<PowerManager>().power = GetComponent<PowerManager>().power;
            spriteRenderer.sprite = off;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (holdButton)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("PlayerPlatform"))
            {
                wire.GetComponent<PowerManager>().power = 0;
                spriteRenderer.sprite = on;
            }
        }
    }
}
