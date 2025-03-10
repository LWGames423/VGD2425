using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform start, end;
    public PowerManager startPower, endPower;
    public float speed;
    public int direction = 1, power;

    private void Update()
    {
        Vector2 target;
        power = startPower.power + endPower.power;

        switch (direction)
        {
            case 0:
                target = start.position;
                break;
            case 1:
                target = end.position;
                break;
            default:
                target = Vector2.zero;
                break;
        }

        if (power >= 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            GetComponent<CapsuleCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
        }

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            switch (direction)
            {
                case 0:
                    direction = 1;
                    break;
                case 1:
                    direction = 0;
                    break;
                default:
                    direction = 0;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && power >= 2)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
