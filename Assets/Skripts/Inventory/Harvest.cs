using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Harvest : MonoBehaviour, Item, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public int itemPrice;
    public string itemName;
    public string itemType;
    public string spritePath;

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

    public void Sale()
    {
        throw new System.NotImplementedException();
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
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
}
