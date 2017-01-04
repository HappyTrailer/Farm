using UnityEngine;
using System.Collections;
using System;

[Serializable]
//клас элемент списка для листа событий
public class fildEvents
{
    int _fild;                //поле относительно которого выполняется событие        
    System.DateTime _time;     // время события
    System.DateTime _timeLeft;
    string _type;              // тип события
    public fildEvents(int id, float t, string s)
    {
        System.DateTime dt = System.DateTime.Now;

        _fild = id;
        _time = dt.AddSeconds(t);
        _type = s;
    }
    public System.DateTime TimeEvent
    {
        get {   return _time; }
        set { _time = value; }
    }
    public int FildId
    {
        get { return _fild; }
    }
    public System.DateTime TimeLeft
    {
        get { return _timeLeft; }
        set { _timeLeft = value; }
    }
    public string FildEvent
    {
        get { return _type; }
    }


}
