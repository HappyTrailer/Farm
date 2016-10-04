using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;

public class Inv : MonoBehaviour {

    public static GameObject inventoryPanel;
    public GameObject inv; 
    public static GameObject actionPanel;
    public GameObject act;
    public static Sead currentSead; 
    public static int currSelect = -1;

    void Start()
    {
        inventoryPanel = inv;
        actionPanel = act;
    }

    public static void FillInventory()
    {
        currSelect = -1;
        List<Item> items = new List<Item>();
        XmlDocument buff;
        TextAsset bindataBuff;
        TextAsset bindata = Resources.Load("XML/Inventory") as TextAsset; 
        XmlDocument invDoc = new XmlDocument();
        invDoc.LoadXml(bindata.text);
        XmlElement root = invDoc.DocumentElement;
        foreach(XmlElement item in root)
        {
            switch(item.ChildNodes[1].InnerText)
            {
                case "sead":
                    buff = new XmlDocument();
                    bindataBuff = Resources.Load("XML/Seads") as TextAsset;
                    buff.LoadXml(bindataBuff.text);
                    XmlElement rootS = buff.DocumentElement;
                    foreach (XmlElement sead in rootS)
                    {
                        if (sead.ChildNodes[0].InnerText == item.ChildNodes[0].InnerText)
                        {
                            items.Add(new SeadInventory() 
                            {
                                Id = System.Convert.ToInt32(sead.ChildNodes[0].InnerText),
                                ItemPrice = System.Convert.ToInt32(sead.ChildNodes[1].InnerText),
                                ItemType = "sead",
                                SpritePath = sead.ChildNodes[2].InnerText,
                                ItemName = sead.ChildNodes[3].InnerText
                            });
                        }
                    }
                    break;
                case "harvest":
                    break;
            }
        }
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if (items.Count > i)
            {
                inventoryPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[i].SpritePath);
                inventoryPanel.transform.GetChild(i).gameObject.AddComponent<Sead>().Init(items[i] as SeadInventory);
            }
        }
    }
}
