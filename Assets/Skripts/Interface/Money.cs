﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Money : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static float money;
    public static GameObject panel;

    public Text moneytxt;

    private Image img;

    void Update()
    {
        panel = GameObject.Find("money");
        moneytxt.text = money.ToString();
        PlayerPrefs.SetFloat("Money", money);
    }

    void Start()
    {
        money = PlayerPrefs.GetFloat("Money");
        if (money < 50)
            money = 500;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
