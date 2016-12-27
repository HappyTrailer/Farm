using UnityEngine;
using System.Collections;

public class MouseTool : MonoBehaviour {

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Inv.inventoryPanel.activeSelf)
            {
                Inv.actionPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
            }
            else if (Shop.shopPanel.activeSelf)
            {
                Shop.shopPanel.SetActive(!Shop.shopPanel.activeSelf);
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            Shop.FillShop();
            ToolsClick.currentTool = "arrow";
            ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow");
            Inv.actionPanel.SetActive(false);
            Inv.filterPanel.SetActive(false);
            Inv.inventoryPanel.SetActive(false);
            Shop.shopPanel.SetActive(!Shop.shopPanel.activeSelf);
        }
        if (Input.GetMouseButtonDown(1))
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
        if (Inv.inventoryPanel.activeSelf == false && Shop.shopPanel.activeSelf == false)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                switch (ToolsClick.currentTool)
                {
                    case "arrow":
                        ToolsClick.currentTool = "dig";
                        break;
                    case "dig":
                        ToolsClick.currentTool = "watering";
                        break;
                    case "watering":
                        ToolsClick.currentTool = "weed";
                        break;
                    case "weed":
                        ToolsClick.currentTool = "vermin";
                        break;
                    case "vermin":
                        ToolsClick.currentTool = "hand";
                        break;
                    case "hand":
                        ToolsClick.currentTool = "arrow";
                        break;
                    case "planted":
                        ToolsClick.currentTool = "arrow";
                        break;
                    case "fertilizer":
                        ToolsClick.currentTool = "arrow";
                        break;
                }
                ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/" + ToolsClick.currentTool);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                switch (ToolsClick.currentTool)
                {
                    case "arrow":
                        ToolsClick.currentTool = "hand";
                        break;
                    case "hand":
                        ToolsClick.currentTool = "vermin";
                        break;
                    case "vermin":
                        ToolsClick.currentTool = "weed";
                        break;
                    case "weed":
                        ToolsClick.currentTool = "watering";
                        break;
                    case "watering":
                        ToolsClick.currentTool = "dig";
                        break;
                    case "dig":
                        ToolsClick.currentTool = "arrow";
                        break;
                    case "planted":
                        ToolsClick.currentTool = "arrow";
                        break;
                    case "fertilizer":
                        ToolsClick.currentTool = "arrow";
                        break;
                }
                ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/" + ToolsClick.currentTool);
            }
        }
	}
}
