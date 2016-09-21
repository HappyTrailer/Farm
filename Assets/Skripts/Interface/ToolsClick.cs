﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;

public class ToolsClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform _cursor;
	public Image CountryImg;
	public GameObject[] tools;
	//private List<GameObject> tools;
	private bool flag = false;
	private Sprite current_img;
	private Image img;

	void Start() 
	{
		img = CountryImg.transform.GetChild (0).GetComponent<Image> ();
		//tools = FindTools("Tool");
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cursor.gameObject.SetActive(false);
        img.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        img.transform.position += new Vector3(20, 20, 20);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cursor.gameObject.SetActive(true);
        img.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        img.transform.position -= new Vector3(20, 20, 20);
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		foreach (GameObject panel in tools)
			panel.SetActive (!flag);
		flag = !flag;
	}

	/*public List<GameObject> FindTools(string tag)
	{	
		List<GameObject> buff = new List<GameObject>();
		Transform[] objs = transform.parent.transform.GetComponentsInChildren<Transform> (true);
		foreach (Transform item in objs) 
		{
			if(item.tag == tag)
				buff.Add(item.gameObject);
		}
		return buff;
	}*/
}