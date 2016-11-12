using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListFildEvent : MonoBehaviour {
    //список запланированых событий
    //осушение грядок появление сорняков,  вредителей 
    

    static List<fildEvents> list;
   
    ListFild listfild = new ListFild();
    // Update is called once per frame
    void Start()
    {
        list = new List<fildEvents>();
      AddFildEvent(0, 300.0f, "123");
    }
    void FixedUpdate()   // В методе апдейт происходит просчет роста растения
    {
        СheckEvent();
    }
    public  void AddFildEvent(int id,float t, string ty)
    {
        
       fildEvents fe = new fildEvents(id,t,ty);
       list.Add(fe);
        Debug.Log("Create event");
    }

    public void СheckEvent()
    {
        if (list.Count > 0)
        {
            Debug.Log("Cheked" + System.DateTime.Now.ToLongTimeString() + "            " + list[0].TimeEvent.ToLongTimeString());
            if (System.DateTime.Now >= list[0].TimeEvent)
            {
                Debug.Log("Event");
                list.Remove(list[0]);
            }
        }
    }


}
