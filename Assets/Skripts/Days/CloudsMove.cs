using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudsMove : MonoBehaviour
{
    public BoxCollider2D Bounds;
    private float size;
    public GameObject[] clouds;
    private SortedList<float, GameObject> cloudsWithSpeed;
	
    void Start ()
    {
        cloudsWithSpeed = new SortedList<float, GameObject>();
        int k = 0;
        foreach (var cloud in clouds)
        {
            cloudsWithSpeed.Add(Random.Range(0.01f, 0.1f), cloud);
        }
        foreach (var cloud in cloudsWithSpeed)
        {
            cloud.Value.GetComponent<SpriteRenderer>().sortingOrder += k;
            k++;
        }
        size = Bounds.bounds.size.x / 2;
    }

	void Update () 
    {
        foreach (var cloud in cloudsWithSpeed)
        {
            if (cloud.Value.transform.position.x >= size + cloud.Value.GetComponent<Renderer>().bounds.size.x / 2)
                cloud.Value.transform.position = new Vector3(-size - cloud.Value.GetComponent<Renderer>().bounds.size.x / 2, cloud.Value.transform.position.y, cloud.Value.transform.position.z);
            cloud.Value.transform.position += new Vector3(cloud.Key * 0.05f, 0, 0);
        }
	}
}
