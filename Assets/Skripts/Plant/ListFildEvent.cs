﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListFildEvent : MonoBehaviour
{
    //список запланированых событий
    //осушение грядок появление сорняков,  вредителей 

    public static List<fildEvents> list;
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

        //fildEvents fe = new fildEvents(id,t,ty);
        list.Add(new fildEvents(id, t, ty));
        list.Sort((a, b) => a.TimeEvent.CompareTo(b.TimeEvent));
        //Debug.Log("=========================");
        //foreach (fildEvents s in list)
        //{
        //    Debug.Log(s.FildEvent);
        //}
        //Debug.Log("Create event");
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
    public void GenEvent(int id, float timeGrow, string type)
    {
        int countEv = Random.Range(0, 2);
        float timeEvent;
        for (int i = 0; i < countEv; i++)
        {
            timeEvent = Random.Range(0, timeGrow);
            AddFildEvent(id, timeEvent, type);
        }
       

    }
    public void DoEvent(string typeEvent, int idFild)
    {
        foreach (Transform child in filds.transform)
        {
            if (child.name == "fild_" + (idFild + 1))
            {
                switch (typeEvent)
                {
                    case "watering":
                    {
                        child.GetComponent<fild>().ChangeWatering(true);
                        break;
                    }
                    case "vermin":
                    {
                        child.GetComponent<fild>().ChangeVermin(true);
                        break;
                    }
                    case "weed":
                    {
                        child.GetComponent<fild>().ChangeWeed(true);
                        break;
                    }
                }
            }
        }
    }

}
