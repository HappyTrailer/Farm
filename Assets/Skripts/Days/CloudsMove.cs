using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudsMove : MonoBehaviour 
{
    public GameObject[] clouds;
    private SortedList<float, GameObject> cloudsWithSpeed;
	
    void Start ()
    {
        cloudsWithSpeed = new SortedList<float, GameObject>();
        int k = 0;
        foreach (var cloud in clouds)
        {
            cloudsWithSpeed.Add(Random.Range(0, 0.1f), cloud);
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
            cloud.Value.transform.position += new Vector3(cloud.Key, 0, 0);
        }
	}
}
