using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FertItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public int id { get; set; }
    public string itemName { get; set; }
    public int itemPrice { get; set; }
    public string itemType { get; set; }
    public float timeFactor { get; set; }
    private bool hovererd = false;

    public void Init(FertItem item)
    {
        id = item.id;
        itemPrice = item.itemPrice;
        timeFactor = item.timeFactor;
        itemName = item.itemName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Money.money >= this.itemPrice)
        {
            Money.money -= this.itemPrice;
            Inv.GetHarvestToInventory(1, this.id, "fertilizer");
            GameObject.Find("Sounds").GetComponent<Sounds>().PlayBuy();
        }
        else
        {
            GameObject.Find("Sounds").GetComponent<Sounds>().PlayFail();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
    }
}
