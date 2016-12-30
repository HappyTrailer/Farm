using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


class Save : MonoBehaviour
{
    public List<SaveField> filds;
    public List<fildEvents> events;

    public void MySave()
    {
        filds = new List<SaveField>();
        events = ListFildEvent.list;
        foreach (Transform child in GameObject.Find("Field").transform)
        {
            filds.Add(child.GetComponent<fild>().Save());
        }
        if (!Directory.Exists(Application.dataPath + "/Saves"))
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        FileStream fs1 = new FileStream(Application.dataPath + "/Saves/fild.sv", FileMode.Create);
        BinaryFormatter formater1 = new BinaryFormatter();
        formater1.Serialize(fs1, filds);
        fs1.Close();
        FileStream fs2 = new FileStream(Application.dataPath + "/Saves/ev.sv", FileMode.Create);
        BinaryFormatter formater2 = new BinaryFormatter();
        formater2.Serialize(fs2, events);
        fs2.Close();
    }

    void Start()
    {
        filds = new List<SaveField>();
        if (File.Exists(Application.dataPath + "/Saves/fild.sv"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/Saves/fild.sv", FileMode.Open);
            BinaryFormatter formater = new BinaryFormatter();
            filds = (List<SaveField>)formater.Deserialize(fs);
            fs.Close();
            int i = 0;
            foreach(Transform child in GameObject.Find("Field").transform)
            {
                child.GetComponent<fild>().Load(filds[i]);
                i++;
            }
        } 
        //------------------------------------------------------------------------------------------------------
        if (File.Exists(Application.dataPath + "/Saves/ev.sv"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/Saves/ev.sv", FileMode.Open);
            BinaryFormatter formater = new BinaryFormatter();
            List<fildEvents> buff = (List<fildEvents>)formater.Deserialize(fs);
            for(int i = 0; i < buff.Count; i++)
            {
                buff[i].TimeEvent = System.DateTime.Now + (System.DateTime.Now - buff[i].TimeEvent);
            }
            ListFildEvent.list = buff;
            fs.Close();
        }
    }
}
