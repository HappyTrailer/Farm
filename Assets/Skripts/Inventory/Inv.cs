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
    public static int k = 0;
    public static int countNext = 0;
    public static List<int> prevIndex = new List<int>();

    void Start()
    {
        currentType = "sead";
        items = Load();
        inventoryPanel = inv;
        actionPanel = act;
        filterPanel = filter;


        ExpBar.current = 0;
        ExpBar.max = 3;
    }

    public static void Select(int id)
    {
        if(currentType == "sead")
            inventoryPanel.transform.GetChild(id).gameObject.GetComponent<Sead>().Select();
        else if (currentType == "harvest")
            inventoryPanel.transform.GetChild(id).gameObject.GetComponent<Harvest>().Select();
    }

    public static void DropItem(int id)
    {
        Inv.items.RemoveAt(id);
    }

    public static void GetHarvestToInventory(int countFruit, int fruitId)
    {
        int curr = -1;
        foreach (Item item in items)
        {
            curr++;
            if (item.ItemType == "harvest" && item.Id == fruitId && item.ItemCount < 10)
            {
                if (items[curr].ItemCount + countFruit > 10)
                {
                    int mod = 10 - items[curr].ItemCount;
                    items[curr].ItemCount += mod;
                    countFruit = countFruit - mod;
                }
                else
                {
                    items[curr].ItemCount += countFruit;
                    countFruit = 0;
                }
            }
        }
        if (countFruit != 0)
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
        if (buff.Count == 0)
        {
            buff.Add(new ItemInInventory() { Id = 1, ItemType = "sead", ItemCount = 10 });
        }
        return buff;
    }

    public static void FillInventory(string page)
    {
        currSelect = -1;
        for (int i = 0; i < items.Count; i++)
        {
            switch (items[i].ItemType)
            {
                case "sead":
                    foreach (PlantItem sead in PlantList.seads)
                    {
                        if (sead.id == items[i].Id)
                        {
                            items[i].ItemPrice = sead.price;
                            items[i].SpritePath = sead.name;
                            items[i].ItemName = sead.name;
                        }
                    }
                    break;
                case "harvest":
                    foreach (PlantItem sead in PlantList.seads)
                    {
                        if (sead.id == items[i].Id)
                        {
                            items[i].ItemPrice = sead.priceFruit;
                            items[i].SpritePath = sead.name;
                            items[i].ItemName = sead.name;
                        }
                    }
                    break;
                case "tool":
                    break;
            }
        }
        if (page == "first")
        {
            k = 0;
            countNext = 0;
        }
        else if (page == "next")
        {
            countNext++;
            prevIndex.Add(inventoryPanel.transform.GetChild(0).gameObject.GetComponent<Harvest>().ItemId);
        }
        else if (page == "prev")
        {
            k = prevIndex[countNext - 1];
            prevIndex.RemoveAt(countNext - 1);
            countNext--;
        }
        else if (page == "current")
        {
            if (Inv.currentType == "harvest")
                k = inventoryPanel.transform.GetChild(0).gameObject.GetComponent<Harvest>().ItemId;
            else if (Inv.currentType == "sead")
                k = inventoryPanel.transform.GetChild(0).gameObject.GetComponent<Sead>().ItemId;
        }
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            inventoryPanel.transform.GetChild(i).GetComponent<Image>().color = new Color32(150, 125, 0, 102);
            inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = "";
            Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Harvest>());
            Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>());
            if (items.Count > k)
            {
                if (items[k].ItemType == currentType)
                {
                    inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = items[k].ItemCount.ToString();
                    items[k].ItemId = k;
                    if (items[k].ItemType == "sead")
                    {
                        inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[k].SpritePath + "/Main");
                        inventoryPanel.transform.GetChild(i).gameObject.AddComponent<Sead>().Init(items[k] as ItemInInventory);
                    }
                    else if(items[k].ItemType == "harvest")
                    {
                        inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[k].SpritePath + "/Fruit");
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

    public static bool NextExists()
    {
        for (int i = k; i < items.Count; i++)
        {
            if(items[i].ItemType == currentType)
                return true;
        }
        return false;
    }

    void OnApplicationQuit()
    {
        Save(items);
    }
}
