using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Sead : MonoBehaviour, Item, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public int itemPrice;
    public string itemName;
    public string itemType;
    public string spritePath;
    public int itemCount;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        Inv.actionPanel.SetActive(true);
        Inv.currSelect = transform.GetSiblingIndex();
        Inv.currentSead = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
