using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class ItemInInventory: Item
{
    public int id;
    public int itemPrice;
    public string itemName;
    public string itemType;
    public string spritePath;
    public int itemCount;

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


    public int ItemCount
    {
        get { return itemCount; }
        set { itemCount = value; }
    }
}
