using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Carousel Entry", menuName = "UI/Carousel Entry")]
public class CarouselEntry : ScriptableObject
{
    [field: SerializeField] public Sprite EntryGraphic { get; private set; }
    [field: SerializeField] public string CharName { get; private set; }
    [field: SerializeField, Multiline(5)] public string Description { get; private set; }

    public void Interact()
    {
        Debug.Log("Carousel Entry interact");
    }
}