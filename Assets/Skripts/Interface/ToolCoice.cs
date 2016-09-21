using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;

public class ToolCoice : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform _cursor;
	public Sprite texture;
	public Image cursor;
	public Image CountryImg;
    private Image img;
    public static string currentTool = "arrow";

    void Start()
    {
        currentTool = "";
        img = CountryImg.transform.GetChild(0).GetComponent<Image>();
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		cursor.sprite = texture;
        currentTool = this.name;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        _cursor.gameObject.SetActive(false);
        img.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
        _cursor.gameObject.SetActive(true);
        img.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
	}
}