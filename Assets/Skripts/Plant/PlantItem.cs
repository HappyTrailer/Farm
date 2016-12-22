using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlantItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int id {get; set;}
    public int price { get; set; }
    public int priceFruit { get; set; }
    public int maxCountFruit { get; set; }
    public int minCountFruit { get; set; }
    public int iterationFruit { get; set; }
    public int countExpiriens { get; set; }
    public int time { get; set; }
    public int level { get; set; }
    public string name { get; set; }
    private bool hovererd = false;

    public void Init(PlantItem item)
    {
        id = item.id;
        price = item.price;
        priceFruit = item.priceFruit;
        maxCountFruit = item.maxCountFruit;
        minCountFruit = item.minCountFruit;
        iterationFruit = item.iterationFruit;
        countExpiriens = item.countExpiriens;
        time = item.time;
        level = item.level;
        name = item.name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Money.money >= this.price && lvl.currentCountlvl >= this.level)
        {
            Money.money -= this.price;
            Inv.GetHarvestToInventory(1, this.id, "sead");
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
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }
}
