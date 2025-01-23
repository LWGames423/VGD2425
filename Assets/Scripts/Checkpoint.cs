using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject spawnPoint;

    private void Awake()
    {
        spawnPoint = GameObject.FindWithTag("Respawn");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawnPoint.transform.position = transform.position;
        }
    }
}
