using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject treeTop;
    public int mirror;

    public void SpawnTreeTop()
    {
        GameObject tree = Instantiate(treeTop, transform.parent);
        tree.transform.localPosition = new Vector3(-0.6f, 23.2f, 0);
        tree.transform.parent = null;
        tree.transform.eulerAngles = new Vector3(0, (mirror == 180) ? 0 : 180, 0);
        tree.GetComponentInChildren<Tree>().mirror = (mirror == 180) ? 0 : 180;
    }
}
