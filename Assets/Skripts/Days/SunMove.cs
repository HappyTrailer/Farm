using UnityEngine;
using System.Collections;
using System;

public class SunMove : MonoBehaviour {

	void Start () {
        
	}

    void Update () {
        transform.position += new Vector3(1, 0.15f);
    }
}
