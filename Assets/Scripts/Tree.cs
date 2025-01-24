using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject treeTop;
    public int mirror;
    public GameObject tree;
    private PlayerManager pm;
    public bool isBase;

    public void SpawnTreeTop()
    {
        tree = Instantiate(treeTop, transform.parent);
        if (isBase)
        {
            tree.transform.localPosition = new Vector3(-0.6f, 11.8f, 0);
        }
        else
        {
            tree.transform.localPosition = new Vector3(-0.6f, 21.8f, 0);
        }
        tree.transform.parent = null;
        tree.transform.eulerAngles = new Vector3(0, (mirror == 180) ? 0 : 180, 0);
        tree.GetComponentInChildren<Tree>().mirror = (mirror == 180) ? 0 : 180;
    }

    public void Kill()
    {
        if (tree)
        {
            tree.GetComponentInChildren<Tree>().Kill();
        }
        GetComponent<Animator>().SetBool("dead", true);
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") && pm.currentCharacter == 3))
        {
            Kill();
        }

        if ((collision.CompareTag("Player") && pm.currentCharacter == 1))
        {
            GetComponent<Animator>().SetBool("grow", true);
        }
    }
}
