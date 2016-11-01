using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudsMove : MonoBehaviour 
{
    public GameObject[] clouds;
    public GameObject left; 
    public GameObject right;
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
    }

	void Update () 
    {
        foreach (var cloud in cloudsWithSpeed)
        {
            if (cloud.Value.transform.position.x >= right.transform.position.x)
                cloud.Value.transform.position = left.transform.position;
            cloud.Value.transform.position += new Vector3(cloud.Key, 0, 0);
        }
	}
}
