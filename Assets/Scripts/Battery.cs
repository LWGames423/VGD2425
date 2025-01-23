using UnityEngine;

public class Battery : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite powered, unpowered;
    public int power = 1;
    public PowerManager wire;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerManager>().currentCharacter == 5)
        {
            wire.power = power;
            spriteRenderer.sprite = powered;
        }
    }
}
