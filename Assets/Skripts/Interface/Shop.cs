using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Shop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject shopP;
    public static GameObject shopPanel;

    void Start()
    {
        shopPanel = shopP;
        FillShop();
    }

    void FillShop()
    {
        List<PlantItem> items = PlantList.seads;
        for (int i = 0; i < shopPanel.transform.GetChild(0).childCount; i++)
        {
            if (items.Count > i)
            {
                shopPanel.transform.GetChild(0).GetChild(i).Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[i].name + "/Main");
                shopPanel.transform.GetChild(0).GetChild(i).Find("Text").GetComponent<Text>().text = items[i].name +
                    ". Цена продажи плодов: " + items[i].priceFruit.ToString() +
                    ". Количество плодов: " + items[i].minCountFruit.ToString() + "-" + items[i].maxCountFruit.ToString() +
                    ". Количество плодонесений: " + items[i].iterationFruit.ToString() +
                    ". Количество опыта за сбор: " + items[i].countExpiriens.ToString() +
                    ". Время роста: " + (items[i].time * 4).ToString() +
                    ". Необходимый уровень плодов: " + items[i].level.ToString();
                shopPanel.transform.GetChild(0).GetChild(i).Find("Buy").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/Buy");
                shopPanel.transform.GetChild(0).GetChild(i).Find("Buy").gameObject.AddComponent<PlantItem>().Init(items[i]);
                shopPanel.transform.GetChild(0).GetChild(i).Find("Price").GetComponent<Text>().text = items[i].price.ToString();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToolsClick.currentTool = "arrow";
        ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow");
        Inv.actionPanel.SetActive(false);
        Inv.filterPanel.SetActive(false);
        Inv.inventoryPanel.SetActive(false);
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
