using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListFildEvent : MonoBehaviour {
    //список запланированых событий
    //осушение грядок появление сорняков,  вредителей 
    

    static List<fildEvents> list;
    // Update is called once per frame
    void Update () {
	
	}
    public static void AddFildEvent(fild f,float t)
    {


        System.DateTime dt = new System.DateTime();
        list.Add(new fildEvents(f, dt));
    }
     
}
