using UnityEngine;
using System.Collections;

public class MouseTool : MonoBehaviour {

	void Update () 
    {
        if (Inv.inventoryPanel.activeSelf == false)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                switch (ToolCoice.currentTool)
                {
                    case "arrow":
                        ToolCoice.currentTool = "dig";
                        break;
                    case "dig":
                        ToolCoice.currentTool = "watering";
                        break;
                    case "watering":
                        ToolCoice.currentTool = "weed";
                        break;
                    case "weed":
                        ToolCoice.currentTool = "vermin";
                        break;
                    case "vermin":
                        ToolCoice.currentTool = "hand";
                        break;
                    case "hand":
                        ToolCoice.currentTool = "arrow";
                        break;
                }
                ToolCoice.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/" + ToolCoice.currentTool);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                switch (ToolCoice.currentTool)
                {
                    case "arrow":
                        ToolCoice.currentTool = "hand";
                        break;
                    case "hand":
                        ToolCoice.currentTool = "vermin";
                        break;
                    case "vermin":
                        ToolCoice.currentTool = "weed";
                        break;
                    case "weed":
                        ToolCoice.currentTool = "watering";
                        break;
                    case "watering":
                        ToolCoice.currentTool = "dig";
                        break;
                    case "dig":
                        ToolCoice.currentTool = "arrow";
                        break;
                }
                ToolCoice.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/" + ToolCoice.currentTool);
            }
        }
	}
}
