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

    void Update()
    {
        if (!hovererd)
        {
            Color a = GetComponent<Image>().color;
            if (Shop.currSelectId != transform.GetSiblingIndex())
                GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.4f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Inv.actionPanel.SetActive(true);
        Inv.actionPanel.transform.GetChild(0).gameObject.SetActive(false);
        Inv.actionPanel.transform.GetChild(1).gameObject.SetActive(false);
        Inv.actionPanel.transform.GetChild(2).gameObject.SetActive(true);
        Shop.currSelectId = transform.GetSiblingIndex();
        Shop.currSelect = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
        hovererd = true;
        Color a = GetComponent<Image>().color;
        if (Shop.currSelectId != transform.GetSiblingIndex())
            GetComponent<Image>().color = new Color(a.r, a.g, a.b, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovererd = false;
        Color a = GetComponent<Image>().color;
        if (Shop.currSelectId != transform.GetSiblingIndex())
            GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.4f);
    }
}
