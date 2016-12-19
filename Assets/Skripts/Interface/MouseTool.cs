using UnityEngine;
using System.Collections;

public class MouseTool : MonoBehaviour {

	void Update () 
    {
        if (Inv.inventoryPanel.activeSelf == false)
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
                }
                ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/" + ToolsClick.currentTool);
            }
        }
	}
}
