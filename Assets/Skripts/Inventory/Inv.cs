using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Inv : MonoBehaviour {

    public static GameObject buyFildPanel;
    public static GameObject inventoryPanel;
    public static GameObject actionPanel;
    public static GameObject filterPanel;
    public static GameObject lockPanel;
    public static GameObject lockPanelInv;
    public static GameObject notifyPanel;

    public GameObject buy; 
    public GameObject inv; 
    public GameObject act;
    public GameObject filter;
    public GameObject _lock;
    public GameObject _lockInv;
    public GameObject notify;

    public static Sead currentSead;
    public static Harvest currentHarv;
    public static Fertilizer currentFert;
    public static fild currFild;

    public static string currentType;

    public static int k = 0;
    public static int countNext = 0;
    public static int currSelect = -1;

    public static List<Item> items;
    public static List<int> prevIndex = new List<int>();

    void Start()
    {
        currentType = "sead";
        items = Load();
        notifyPanel = notify;
        lockPanel = _lock;
        lockPanelInv = _lockInv;
        buyFildPanel = buy;
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
        else if (currentType == "fertilizer")
            inventoryPanel.transform.GetChild(id).gameObject.GetComponent<Fertilizer>().Select();
    }

    public static void DropItem(int id)
    {
        Inv.items.RemoveAt(id);
    }

    public static void GetHarvestToInventory(int countFruit, int fruitId, string type)
    {
        int curr = -1;
        foreach (Item item in items)
        {
            curr++;
            if (item.ItemType == type && item.Id == fruitId && item.ItemCount < 10)
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
                        items.Add(new ItemInInventory() { Id = fruitId, ItemType = type, ItemCount = 10 });
                        countFruit = countFruit - 10;
                    }
                    else
                    {
                        items.Add(new ItemInInventory() { Id = fruitId, ItemType = type, ItemCount = countFruit });
                        countFruit = 0;
                    }
                } while (countFruit != 0);
            }
            else
            {
                items.Add(new ItemInInventory() { Id = fruitId, ItemType = type, ItemCount = countFruit });
            }
        }
    }

    public static void SaveInv(List<Item> list)
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
            buff.Add(new ItemInInventory() { Id = 1, ItemType = "fertilizer", ItemCount = 10 });
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
                case "fertilizer":
                    foreach (FertItem sead in PlantList.ferts)
                    {
                        if (sead.id == items[i].Id)
                        {
                            items[i].ItemPrice = sead.itemPrice;
                            items[i].SpritePath = sead.itemName;
                            items[i].ItemName = sead.itemName;
                        }
                    }
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
            prevIndex.Add(inventoryPanel.transform.GetChild(0).gameObject.GetComponent<Harvest>().ItemId);//////
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
            else if (Inv.currentType == "fertilizer")
                k = inventoryPanel.transform.GetChild(0).gameObject.GetComponent<Sead>().ItemId;
        }
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            inventoryPanel.transform.GetChild(i).GetComponent<Image>().color = new Color32(150, 125, 0, 102);
            inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/UIMask");
            inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = "";
            Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Harvest>());
            Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Sead>());
            Destroy(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<Fertilizer>());
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
                    else if (items[k].ItemType == "fertilizer")
                    {
                        inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[k].SpritePath);
                        inventoryPanel.transform.GetChild(i).gameObject.AddComponent<Fertilizer>().Init(items[k] as ItemInInventory);
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
        SaveInv(items);
    }

    public void CancelBuy()
    {
        Inv.buyFildPanel.SetActive(false);
        Inv.lockPanelInv.SetActive(false);
    }

    public void ApplyBuy()
    {
        if(Money.money >= currFild.idFild * 10 * 1.5)
        {
            Money.money -= (float)(currFild.idFild * 10 * 1.5);
            GameObject.Find("Sounds").GetComponent<Sounds>().PlayBuy();
            Inv.buyFildPanel.SetActive(false);
            currFild.locked = false;
            currFild.GetComponent<SpriteRenderer>().sprite = currFild.sandField;
            currFild.transform.GetChild(3).gameObject.SetActive(false);
            if (fild.nextFild <= 48)
            {
                fild.nextFild++;
                PlayerPrefs.SetFloat("NextFild", fild.nextFild);
                GameObject.Find("Field").transform.GetChild(fild.nextFild).transform.GetChild(3).gameObject.SetActive(true);
            }
            Inv.lockPanelInv.SetActive(false);
        }
        else
            GameObject.Find("Sounds").GetComponent<Sounds>().PlayFail();
    }
}
