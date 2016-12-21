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

    public void Init(ItemInInventory sead)
    {
        this.Id = sead.Id;
        this.ItemPrice = sead.ItemPrice;
        this.ItemType = sead.ItemType;
        this.SpritePath = sead.SpritePath;
        this.ItemName = sead.ItemName;
        this.ItemCount = sead.ItemCount;
        this.ItemId = sead.ItemId;
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


    public int ItemCount
    {
        get { return itemCount; }
        set { itemCount = value; }
    }
}