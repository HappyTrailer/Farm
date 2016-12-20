using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;

public class ToolsClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image CountryImg;
	public GameObject[] tools;
	//private List<GameObject> tools;
	private bool flag = false;
	private Sprite current_img;
	private Image img;
    private Image seno;
    private bool hover = false;
    public static Image globalCursor;
    public static string currentTool = "arrow";

	void Start()
    {
        globalCursor = GameObject.Find("Cursor").GetComponent<Image>();
        seno = CountryImg.transform.GetChild(0).GetComponent<Image>();
		img = CountryImg.transform.GetChild(1).GetComponent<Image>();
		//tools = FindTools("Tool");
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Inv.inventoryPanel.activeSelf == false && Shop.shopPanel.activeSelf == false)
        {
            GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
            if (!hover)
            {
                seno.gameObject.SetActive(true);
            }
            seno.transform.position += new Vector3(20, 20, 20);
            img.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            img.transform.position += new Vector3(20, 20, 20);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!hover)
        {
            seno.gameObject.SetActive(false);
        }
        seno.transform.position -= new Vector3(20, 20, 20);
        img.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        img.transform.position -= new Vector3(20, 20, 20);
    }

	public void OnPointerClick(PointerEventData eventData)
    {
        hover = !hover;
        foreach (GameObject panel in tools)
            panel.SetActive(!flag);
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