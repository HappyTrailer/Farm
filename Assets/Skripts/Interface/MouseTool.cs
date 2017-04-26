using UnityEngine;
using System.Collections;

public class MouseTool : MonoBehaviour {

	void Update ()
    {
        if (Input.GetMouseButtonDown(2) && GameMenu.settingsPanel.activeSelf == false && GameMenu.menuPanel.activeSelf == false)
        {
            ToolsClick.currentTool = "arrow";
            Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/arrow2"), Vector2.zero, CursorMode.Auto);
            if (Shop.shopPanel.activeSelf == false)
            {
                Inv.actionPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                Inv.buyFildPanel.SetActive(false);
                Shop.shopPanel.SetActive(true);
                Inv.lockPanelInv.SetActive(true);
                Shop.FillShop();
            }
            else
            {
                Shop.shopPanel.SetActive(false);
                Inv.lockPanelInv.SetActive(false);
            }
        }
        if (Input.GetMouseButtonDown(1) && GameMenu.settingsPanel.activeSelf == false && GameMenu.menuPanel.activeSelf == false)
        {
            ToolsClick.currentTool = "arrow";
            Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/arrow2"), Vector2.zero, CursorMode.Auto);
            if (Inv.inventoryPanel.activeSelf == false)
            {
                Inv.lockPanelInv.SetActive(true);
                Inv.inventoryPanel.SetActive(true);
                Inv.filterPanel.SetActive(true);
                Shop.shopPanel.SetActive(false);
                Inv.buyFildPanel.SetActive(false);
                Inv.FillInventory("first");
            }
            else
            {
                Inv.lockPanelInv.SetActive(false);
                Inv.actionPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
            }
        }
        if (Inv.inventoryPanel.activeSelf == false && Shop.shopPanel.activeSelf == false
             && GameMenu.settingsPanel.activeSelf == false && GameMenu.menuPanel.activeSelf == false && Inv.buyFildPanel.activeSelf == false)
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
                Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/" + ToolsClick.currentTool + "2"), Vector2.zero, CursorMode.Auto);
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
                Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/" + ToolsClick.currentTool + "2"), Vector2.zero, CursorMode.Auto);
            }
        }
	}
}
