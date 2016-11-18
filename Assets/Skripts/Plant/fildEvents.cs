using UnityEngine;
using System.Collections;

//клас элемент списка для листа событий
public class fildEvents : MonoBehaviour {
    int _fild;                //поле относительно которого выполняется событие        
    System.DateTime _time;     // время события
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
    }
    public int FildId
    {
        get { return _fild; }
    }
    public string FildEvent
    {
        get { return _type; }
    }

}
