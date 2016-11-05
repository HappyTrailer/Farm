using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class Inv : MonoBehaviour {

    public static GameObject inventoryPanel;
    public GameObject inv; 
    public static GameObject actionPanel;
    public GameObject act;
    public static Sead currentSead; 
    public static int currSelect = -1;
    public static List<Item> items;

    void Start()
    {
        items = Load();
        inventoryPanel = inv;
        actionPanel = act;
    }

    public static void DropItem(int id)
    {
        Inv.items.RemoveAt(id);
    }

    public static void GetHarvestToInventory(int countFruit, int fruitId)
    {
        bool isExist = false;
        int curr = -1;
        foreach (Item item in items)
        {
            curr++;
            if (item.ItemType == "harvest" && item.Id == fruitId)
            {
                isExist = true;
                break;
            }
        }
        if (isExist)
        {
            items[curr].ItemCount += countFruit;
            Debug.Log(items[curr].ItemCount);
        }
        else
        {
            items.Add(new ItemInInventory() { Id = fruitId, ItemType = "harvest", ItemCount = countFruit });
        }
    }

    public static void Save(List<Item> list)
    {
        if (!Directory.Exists(Application.dataPath + "/Saves"))
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        FileStream fs = new FileStream(Application.dataPath + "/Saves/inv.sv", FileMode.Create);
        BinaryFormatter formater = new BinaryFormatter();
        formater.Serialize(fs, list);
        fs.Close();
    }

    public static List<Item> Load()
    {
        List<Item> buff = new List<Item>();
        if (File.Exists(Application.dataPath + "/Saves/inv.sv"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/Saves/inv.sv", FileMode.Open);
            BinaryFormatter formater = new BinaryFormatter();
            buff = (List<Item>)formater.Deserialize(fs);
            fs.Close();
        }
        if (buff.Count == 0)
        {
            buff.Add(new ItemInInventory() { Id = 1, ItemType = "sead", ItemCount = 5 });
            buff.Add(new ItemInInventory() { Id = 2, ItemType = "sead", ItemCount = 5 });
        }
        return buff;
    }

    public static void FillInventory()
    {
        currSelect = -1;
        XmlDocument buff;
        TextAsset bindataBuff;
        for (int i = 0; i < items.Count; i++)
        {
            switch (items[i].ItemType)
            {
                case "sead":
                    buff = new XmlDocument();
                    bindataBuff = Resources.Load("XML/Seads") as TextAsset;
                    buff.LoadXml(bindataBuff.text);
                    XmlElement rootS = buff.DocumentElement;
                    foreach (XmlElement sead in rootS)
                    {
                        if (sead.ChildNodes[0].InnerText == items[i].Id.ToString())
                        {
                            items[i].ItemPrice = System.Convert.ToInt32(sead.ChildNodes[1].InnerText);
                            items[i].SpritePath = sead.ChildNodes[2].InnerText;
                            items[i].ItemName = sead.ChildNodes[3].InnerText;
                        }
                    }
                    break;
                case "harvest":
                    buff = new XmlDocument();
                    bindataBuff = Resources.Load("XML/Harvest") as TextAsset;
                    buff.LoadXml(bindataBuff.text);
                    XmlElement rootH = buff.DocumentElement;
                    foreach (XmlElement sead in rootH)
                    {
                        if (sead.ChildNodes[0].InnerText == items[i].Id.ToString())
                        {
                            items[i].ItemPrice = System.Convert.ToInt32(sead.ChildNodes[1].InnerText);
                            items[i].SpritePath = sead.ChildNodes[2].InnerText;
                            items[i].ItemName = sead.ChildNodes[3].InnerText;
                        }
                    }
                    break;
            }
        }
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if (items.Count > i)
            {
                inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[i].SpritePath);
                inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = items[i].ItemCount.ToString();
                if (inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>() != null)
                    Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>());
                inventoryPanel.transform.GetChild(i).gameObject.AddComponent<Sead>().Init(items[i] as ItemInInventory);
            }
            else
            {
                inventoryPanel.transform.GetChild(i).GetComponent<Image>().color = new Color32(150, 125, 0, 102);
                inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
                inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = "";
                if (inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>() != null)
                    Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>());
            }

        }
    }

    void OnApplicationQuit()
    {
        Save(items);
    }
}
