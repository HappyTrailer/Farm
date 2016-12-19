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
    public static PlantItem currSelect;
    public static int currSelectId;

    void Start()
    {
        shopPanel = shopP;
        FillShop();
    }

    void FillShop()
    {
        currSelectId = -1;
        List<PlantItem> items = PlantList.seads;
        for (int i = 0; i < shopPanel.transform.childCount; i++)
        {
            shopPanel.transform.GetChild(i).GetComponent<Image>().color = new Color32(150, 125, 0, 102);
            shopPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            shopPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = "";
            Destroy(shopPanel.transform.GetChild(i).gameObject.GetComponent<PlantItem>());
            if (items.Count > i)
            {
                shopPanel.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<Text>().text = items[i].price.ToString();
                shopPanel.transform.GetChild(i).GetChild(0).transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Plant/" + items[i].name + "/Main");
                shopPanel.transform.GetChild(i).gameObject.AddComponent<PlantItem>().Init(items[i]);
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
        currSelectId = -1;
        Inv.actionPanel.SetActive(false);
        Inv.filterPanel.SetActive(false);
        Inv.inventoryPanel.SetActive(false);
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
