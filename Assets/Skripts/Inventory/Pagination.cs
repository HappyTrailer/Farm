﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Pagination : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
        Inv.actionPanel.SetActive(false);
        switch (this.name)
        {
            case "Left":
                if(Inv.countNext > 0)
                    Inv.FillInventory("prev");
                break;
            case "Right":
                if (Inv.NextExists())
                    Inv.FillInventory("next");
                break;
        }
    }
}