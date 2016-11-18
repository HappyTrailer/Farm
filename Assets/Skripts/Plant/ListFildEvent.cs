using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListFildEvent : MonoBehaviour
{
    //список запланированых событий
    //осушение грядок появление сорняков,  вредителей 


    static List<fildEvents> list;
    public GameObject filds;
    //ListFild listfild;
    // Update is called once per frame
    void Start()
    {
        list = new List<fildEvents>();
        // listfild = new ListFild();
    }
    void FixedUpdate()   // В методе апдейт происходит просчет роста растения
    {
        СheckEvent();
    }
    public void AddFildEvent(int id, float t, string ty)
    {

        // fildEvents fe = new fildEvents(id,t,ty);
        list.Add(new fildEvents(id, t, ty));
        Debug.Log("Create event");
    }

    public void СheckEvent()
    {
        if (list.Count > 0)
        {
            // Debug.Log("Cheked "+ list[0].FildId + " " + System.DateTime.Now.ToLongTimeString() + "            " + list[0].TimeEvent.ToLongTimeString());
            if (System.DateTime.Now >= list[0].TimeEvent)
            {
                //Debug.Log("Event "+ list[0].FildId + " "+ list[0].FildEvent + " done!");
                string ev = list[0].FildEvent;
                int idf = list[0].FildId;
                DoEvent(ev, idf);
                list.Remove(list[0]);
            }
        }
    }

    public void DoEvent(string typeEvent, int idFild)
    {
        foreach (Transform child in filds.transform)
        {
            if (child.name == "fild_" + (idFild + 1))
            {
                Debug.Log("1111111111111");


                switch (typeEvent)
                {

                    case "watering":
                        {
                            //listfild.filds[0].GetComponent<fild>().ChangeWatering();
                            //listfild.GetFild(idFild);
                            child.GetComponent<fild>().ChangeWatering();

                            break;
                        }
                    case "vermin":
                        {
                            break;
                        }
                    case "weed":
                        {
                            break;
                        }
                }
            }
        }
    }


}
