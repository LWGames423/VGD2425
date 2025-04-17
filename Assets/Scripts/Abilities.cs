using UnityEngine;

public class Abilities : MonoBehaviour
{
    public PlayerManager pm;

    public GameObject steel;
    public GameObject charcoal;

    private int character;

    private void Start()
    {
        character = pm.currentCharacter;
    }

    private void Update()
    {
        character = pm.currentCharacter;
        if (Input.GetKeyDown(KeyCode.Space) && pm.timer > 0.5f)
        {
            switch (character)
            {
                case 0:
                    GameObject platform = Instantiate(steel, transform);
                    platform.transform.parent = null;
                    platform.layer = 6;
                    pm.canMove = false;
                    pm.Respawn();
                    break;

                case 2:
                    GetComponent<Animator>().SetBool("ability", true);
                    pm.canMove = false;
                    break;

                default:
                    pm.canMove = false;
                    pm.Respawn();
                    break;
            }
        }
        if (pm.canMove == false && pm.timer > .7f) {
            pm.canMove = true;
        }
    }

    public void SpawnTree()
    {
        GameObject platform = Instantiate(steel, transform);
        platform.transform.localPosition = new Vector3(0, 3.7f, 0);
        platform.transform.parent = null;
        platform.layer = 6;

        GetComponent<Animator>().SetBool("ability", false);
        pm.Respawn();
    }

    public void BecomeCharcoal()
    {
        GameObject character = Instantiate(charcoal, transform);
        character.transform.localPosition = Vector3.zero;
        character.transform.parent = null;
        Destroy(gameObject);
    }
}
