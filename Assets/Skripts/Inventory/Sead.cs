using UnityEngine;
using System.Collections;

public class Sead : MonoBehaviour, Item {

    public int id;
    public int itemPrice;
    public string itemName;
    public string itemType;
    public string spritePath;

    public int ItemPrice
    {
        get { return itemPrice; }
        set { itemPrice = value; }
    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public string ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }

    public string SpritePath
    {
        get { return spritePath; }
        set { spritePath = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public void Sale()
    {
        throw new System.NotImplementedException();
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public Plant GetPlantFromSead()
    {
        Plant pl = new Plant();
        return pl;
    }

    public void Init(Sead sead)
    {
        this.Id = sead.Id;
        this.ItemPrice = sead.ItemPrice;
        this.ItemType = sead.ItemType;
        this.SpritePath = sead.SpritePath;
        this.ItemName = sead.ItemName;
    }
}
