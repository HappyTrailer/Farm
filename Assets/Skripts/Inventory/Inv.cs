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
    public static GameObject filterPanel;
    public GameObject filter;
    public static Sead currentSead;
    public static string currentType;
    public static Harvest currentHarv;
    public static int currSelect = -1;
    public static List<Item> items;

    void Start()
    {
        currentType = "sead";
        items = Load();
        inventoryPanel = inv;
        actionPanel = act;
        filterPanel = filter;
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
            if (item.ItemType == "harvest" && item.Id == fruitId && item.ItemCount < 10)
            {
                isExist = true;
                break;
            }
        }
        if (isExist)
        {
            if (items[curr].ItemCount + countFruit > 10)
            {
                int mod = 10 - items[curr].ItemCount;
                items[curr].ItemCount += mod;
                countFruit = countFruit - mod;
                do
                {
                    if (countFruit >= 10)
                    {
                        items.Add(new ItemInInventory() { Id = fruitId, ItemType = "harvest", ItemCount = 10 });
                        countFruit = countFruit - 10;
                    }
                    else
                    {
                        items.Add(new ItemInInventory() { Id = fruitId, ItemType = "harvest", ItemCount = countFruit });
                        countFruit = 0;
                    }
                } while (countFruit != 0);
            }
            else
            {
                items[curr].ItemCount += countFruit;
            }
        }
        else
        {
            if (countFruit > 10)
            {
                do
                {
                    if (countFruit >= 10)
                    {
                        items.Add(new ItemInInventory() { Id = fruitId, ItemType = "harvest", ItemCount = 10 });
                        countFruit = countFruit - 10;
                    }
                    else
                    {
                        items.Add(new ItemInInventory() { Id = fruitId, ItemType = "harvest", ItemCount = countFruit });
                        countFruit = 0;
                    }
                } while (countFruit != 0);
            }
            else
            {
                items.Add(new ItemInInventory() { Id = fruitId, ItemType = "harvest", ItemCount = countFruit });
            }
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
        /*if (buff.Count == 0)
        {
            buff.Add(new ItemInInventory() { Id = 1, ItemType = "sead", ItemCount = 5 });
            buff.Add(new ItemInInventory() { Id = 2, ItemType = "sead", ItemCount = 5 });
        }*/
        return buff;
    }

    public static void FillInventory(string page)
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
                case "tool":
                    break;
            }
        }
        int k = 0;
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            inventoryPanel.transform.GetChild(i).GetComponent<Image>().color = new Color32(150, 125, 0, 102);
            inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = "";
            if (inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>() != null)
                Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>());
            if (items.Count > k)
            {
                if (items[k].ItemType == currentType)
                {
                    if (items[k].ItemType == "sead")
                    {
                        inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[k].SpritePath);
                        inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = items[k].ItemCount.ToString();
                        if (inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>() != null)
                            Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>());
                        items[k].ItemId = k;
                        inventoryPanel.transform.GetChild(i).gameObject.AddComponent<Sead>().Init(items[k] as ItemInInventory);
                    }
                    else if(items[k].ItemType == "harvest")
                    {
                        inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[k].SpritePath);
                        inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = items[k].ItemCount.ToString();
                        if (inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Harvest>() != null)
                            Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Harvest>());
                        items[k].ItemId = k;
                        inventoryPanel.transform.GetChild(i).gameObject.AddComponent<Harvest>().Init(items[k] as ItemInInventory);
                    }
                }
                else
                {
                    i--;
                }
            }
            k++;
        }
    }

    void OnApplicationQuit()
    {
        Save(items);
    }
}
