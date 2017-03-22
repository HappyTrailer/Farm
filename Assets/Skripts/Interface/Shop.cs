using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject shopP;
    public static GameObject shopPanel;

    void Start()
    {
        shopPanel = shopP;
    }

    public static void FillShop()
    {
        List<PlantItem> itemsS = PlantList.seads;
        int k = 0;
        for (int i = 0; i < itemsS.Count; i++)
        {
            Destroy(shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Button").gameObject.GetComponent<PlantItem>());
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Image").Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + itemsS[i].name + "/Main");
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Text").Find("Text").GetComponent<Text>().text = itemsS[i].name +
                ".\n Время роста: " + ((itemsS[i].time * 4) /60).ToString() + " мин.";
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Buy").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/Buy");
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Price").GetComponent<Text>().text = itemsS[i].price.ToString();
            if (ExpBar.currentCountlvl >= itemsS[i].level)
            {
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Button").gameObject.AddComponent<PlantItem>().Init(itemsS[i]);
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Lock").gameObject.SetActive(false);
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(150, 125, 0, 102);
            }
            else
            {
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Text").gameObject.SetActive(false);
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Button").gameObject.GetComponent<Button>().enabled = false;
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Lock").gameObject.SetActive(true);
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(150, 125, 0, 255);
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Button").GetComponent<Image>().color = new Color32(150, 125, 0, 255);
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Image").Find("Image").GetComponent<Image>().color = new Color32(150, 125, 0, 255);
                shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Image").GetComponent<Image>().color = new Color32(150, 125, 0, 255);
            }
            k++;
        }
        List<FertItem> itemsF = PlantList.ferts;
        for (int i = 0; i < itemsF.Count; i++)
        {
            Destroy(shopPanel.transform.GetChild(0).GetChild(0).GetChild(i).Find("Button").gameObject.GetComponent<FertItem>());
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(k + i).Find("Image").Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + itemsF[i].itemName);
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(k + i).Find("Text").Find("Text").GetComponent<Text>().text = itemsF[i].itemName +
                ". \nУскорение роста: " + (itemsF[i].timeFactor * 100).ToString() + "%.";
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(k + i).Find("Buy").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/Buy");
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(k + i).Find("Price").GetComponent<Text>().text = itemsF[i].itemPrice.ToString();
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(k + i).Find("Button").gameObject.AddComponent<FertItem>().Init(itemsF[i]);
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(k + i).Find("Lock").gameObject.SetActive(false);
            shopPanel.transform.GetChild(0).GetChild(0).GetChild(k + i).GetComponent<Image>().color = new Color32(150, 125, 0, 102);
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
        if (shopPanel.activeSelf == false)
        {
            Inv.actionPanel.SetActive(false);
            Inv.filterPanel.SetActive(false);
            Inv.inventoryPanel.SetActive(false);
            Inv.buyFildPanel.SetActive(false);
            shopPanel.SetActive(true);
            Inv.lockPanelInv.SetActive(true);
            FillShop();
        }
        else
        {
            shopPanel.SetActive(false);
            Inv.lockPanelInv.SetActive(false);
        }
    }
}
