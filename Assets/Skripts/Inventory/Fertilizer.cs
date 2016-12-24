using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Fertilizer : MonoBehaviour, Item, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int item_id;
    public int id;
    public int itemPrice;
    public string itemName;
    public string itemType;
    public string spritePath;
    public int itemCount;
    public float timeFactor;

    private bool hovererd = false;

    public int ItemPrice
    {
        get { return itemPrice; }
        set { itemPrice = value; }
    }

    public float TimeFactor
    {
        get { return timeFactor; }
        set { timeFactor = value; }
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

    public int ItemId
    {
        get { return item_id; }
        set { item_id = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public int ItemCount
    {
        get { return itemCount; }
        set { itemCount = value; }
    }

    public void Init(ItemInInventory fert)
    {
        this.Id = fert.Id;
        this.ItemPrice = fert.ItemPrice;
        this.ItemType = fert.ItemType;
        this.SpritePath = fert.SpritePath;
        this.ItemName = fert.ItemName;
        this.ItemCount = fert.ItemCount;
        this.ItemId = fert.ItemId;
        this.TimeFactor = PlantList.ferts[fert.Id - 1].timeFactor;
    }

    void Update()
    {
        if (!hovererd)
        {
            Color a = GetComponent<Image>().color;
            if (Inv.currSelect != transform.GetSiblingIndex())
                GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.4f);
        }
    }

    public void Select()
    {
        Color a = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(a.r, a.g, a.b, 1);
        Inv.currSelect = transform.GetSiblingIndex();
        Inv.currentFert = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Inv.actionPanel.SetActive(true);
        Inv.actionPanel.transform.GetChild(0).gameObject.SetActive(true);
        Inv.actionPanel.transform.GetChild(1).gameObject.SetActive(true);
        Inv.currSelect = transform.GetSiblingIndex();
        Inv.currentFert = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
        hovererd = true;
        Color a = GetComponent<Image>().color;
        if (Inv.currSelect != transform.GetSiblingIndex())
            GetComponent<Image>().color = new Color(a.r, a.g, a.b, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovererd = false;
        Color a = GetComponent<Image>().color;
        if (Inv.currSelect != transform.GetSiblingIndex())
            GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.4f);
    }
}