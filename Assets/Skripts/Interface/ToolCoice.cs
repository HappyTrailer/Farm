using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;

public class ToolCoice : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Sprite texture;
    public Image cursor; 
    public static Image globalCursor;
	public Image CountryImg;
    private Image img;
    private Image seno;
    public static string currentTool = "arrow";
    private bool hover = false;

    void Start()
    {
        globalCursor = cursor;
        seno = CountryImg.transform.GetChild(0).GetComponent<Image>();
        img = CountryImg.transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        if (currentTool == this.name)
            seno.gameObject.SetActive(true);
        seno.transform.Rotate(Vector3.forward * -1);
        if(seno.gameObject.activeSelf && ToolCoice.currentTool != this.name && !hover)
            seno.gameObject.SetActive(false);
    }

	public void OnPointerClick(PointerEventData eventData)
    {
        cursor.sprite = texture;
        currentTool = this.name;
        if (this.name == "inventory")
        {
            if (Inv.inventoryPanel.activeSelf == false)
            {
                Inv.inventoryPanel.SetActive(true);
                Inv.FillInventory();
            }
            else
            {
                currentTool = "arrow";
                Inv.actionPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
            }
        }
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        hover = true;
        seno.gameObject.SetActive(true);
        img.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
	}
	
	public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
        if (ToolCoice.currentTool != this.name)
            seno.gameObject.SetActive(false);
        img.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
	}
}