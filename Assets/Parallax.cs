using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;

    float startPos, startPosY;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float distance2 = cam.transform.position.y;

        transform.position = new Vector3(startPos + distance, distance2, transform.position.z);
    }
}
