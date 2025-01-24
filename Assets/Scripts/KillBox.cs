using UnityEngine;

public class KillBox : MonoBehaviour
{
    private bool active;
    private PlayerMovement pm;
    private float timer;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            active = true;
            pm = collision.GetComponent<PlayerMovement>();
            timer = 0;
        }
    }

    private void Update()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        if (active)
        {
            timer += Time.deltaTime;

            if (timer > 0.5f)
            {
                pm.Respawn();
                active = false;
                timer = 0;
            }
            else if (timer > 1f)
            {
                active = false;
                timer = 0;
            }
        }
    }
}
