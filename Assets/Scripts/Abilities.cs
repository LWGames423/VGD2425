using UnityEngine;

public class Abilities : MonoBehaviour
{
    public PlayerManager pm;

    public GameObject steel;

    private int character;

    private void Start()
    {
        character = pm.currentCharacter;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (character)
            {
                case 0:
                    GameObject platform = Instantiate(steel, transform);
                    platform.transform.parent = null;
                    platform.layer = 6;

                    pm.Respawn();
                    break;

                default:
                    pm.Respawn();
                    break;
            }
        }
    }
}
