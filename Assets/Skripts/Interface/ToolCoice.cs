using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;

public class ToolCoice : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Sprite texture;
	public Image CountryImg;
    private Image img;
    private Image seno;
    private bool hover = false;

    void Start()
    {
        seno = CountryImg.transform.GetChild(0).GetComponent<Image>();
        img = CountryImg.transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        if (ToolsClick.currentTool == this.name)
            seno.gameObject.SetActive(true);
        seno.transform.Rotate(Vector3.forward * -1);
        if (seno.gameObject.activeSelf && ToolsClick.currentTool != this.name && !hover)
            seno.gameObject.SetActive(false);
    }

	public void OnPointerClick(PointerEventData eventData)
    {
        if (Inv.inventoryPanel.activeSelf == false && Shop.shopPanel.activeSelf == false)
        {
            if (this.name == "inventory")
            {
                ToolsClick.currentTool = "arrow";
                ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow");
                if (Inv.inventoryPanel.activeSelf == false)
                {
                    Inv.inventoryPanel.SetActive(true);
                    Inv.filterPanel.SetActive(true);
                    Shop.shopPanel.SetActive(false);
                    Inv.FillInventory("first");
                }
                else
                {
                    Inv.actionPanel.SetActive(false);
                    Inv.filterPanel.SetActive(false);
                    Inv.inventoryPanel.SetActive(false);
                }
            }
            else
            {
                ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/" + this.name);
                ToolsClick.currentTool = this.name;
            }
        }
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
        hover = true;
        seno.gameObject.SetActive(true);
        img.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
	}
	
	public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
        if (ToolsClick.currentTool != this.name)
            seno.gameObject.SetActive(false);
        img.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
	}
}