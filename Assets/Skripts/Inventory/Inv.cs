using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inv : MonoBehaviour {

    public static GameObject inventoryPanel;
    public GameObject inv;
    public Sprite buff;
    List<Item> items;

    void Start()
    {
        items = new List<Item>();
        inventoryPanel = inv;
        FillInventory();
    }

    void FillInventory()
    {
        items.Add(new Sead() { Id = 1, ItemPrice = 50, ItemType = "sead", SpritePath = "pl1", ItemName = "Apple" });
        items.Add(new Sead() { Id = 2, ItemPrice = 70, ItemType = "sead", SpritePath = "pl2", ItemName = "Tomat" });
        items.Add(new Sead() { Id = 3, ItemPrice = 20, ItemType = "sead", SpritePath = "pl3", ItemName = "Potato" });
        items.Add(new Sead() { Id = 4, ItemPrice = 35, ItemType = "sead", SpritePath = "pl4", ItemName = "Lime" });
        items.Add(new Sead() { Id = 5, ItemPrice = 99, ItemType = "sead", SpritePath = "pl5", ItemName = "Grusha" });
        for(int i = 0; i < inv.transform.childCount; i++)
        {
            if (items.Count > i)
            {
                inv.transform.GetChild(i).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[i].SpritePath);
                inv.transform.GetChild(i).gameObject.AddComponent<Sead>();
            }
        }
    }
}
