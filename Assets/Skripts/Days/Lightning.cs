using UnityEngine;
using System.Collections;
using System;

public class Lightning : MonoBehaviour {

    public Transform tr;
    public float speed;
    private Light light;
    private int hour;

	void Start () {
        light = gameObject.GetComponent<Light>();
	}
	
	void Update () {
        hour = DateTime.Now.Hour;
        transform.RotateAround(tr.position, Vector3.down, speed);
	}
}
