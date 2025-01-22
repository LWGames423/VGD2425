using UnityEngine;

public class NewCharacterEntry : MonoBehaviour
{
    [SerializeField]
    private CarouselEntry newCharacter;

    private Carousel carousel;
    private bool entryAdded;

    private void Start()
    {
        entryAdded = false;
        carousel = GameObject.FindGameObjectWithTag("Carousel").GetComponent<Carousel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && entryAdded == false)
        {
            carousel.AddEntries(newCharacter);
            entryAdded = true;
        }
    }
}
