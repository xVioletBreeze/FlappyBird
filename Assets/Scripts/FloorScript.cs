using UnityEngine;

public class floorScript : MonoBehaviour
{
    public GameObject floor1;
    public GameObject floor2;
    public float scrollSpeed = 2f;

    private float tileWidth;
    void Start()
    {
        tileWidth = floor1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        floor1.transform.position += scrollSpeed * Time.deltaTime * Vector3.left;
        floor2.transform.position += scrollSpeed * Time.deltaTime * Vector3.left;

        if (floor1.transform.position.x <= -tileWidth)
        {
            floor1.transform.position = new Vector3(floor2.transform.position.x + tileWidth, floor1.transform.position.y, floor1.transform.position.z);
        }

        if (floor2.transform.position.x <= -tileWidth)
        {
            floor2.transform.position = new Vector3(floor1.transform.position.x + tileWidth, floor2.transform.position.y, floor2.transform.position.z);
        }
    }
}
