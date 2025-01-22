    using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredPower;
    public PowerManager pm;

    private void Update()
    {
        if (pm.power >= requiredPower)
        {
            GetComponent<Animator>().SetBool("Open", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<Animator>().SetBool("Open", false);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
