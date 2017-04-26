using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ActionPanel : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(this.name)
        {
            case "Sell":
                int itemId = 0;
                if(Inv.currentType == "harvest")
                    itemId = Inv.currentHarv.ItemId;
                else if(Inv.currentType == "sead")
                    itemId = Inv.currentSead.ItemId;
                else if (Inv.currentType == "fertilizer")
                    itemId = Inv.currentFert.ItemId;
                Money.money += Inv.items[itemId].ItemPrice;
                Inv.items[itemId].ItemCount -= 1;
                if (Inv.items[itemId].ItemCount <= 0)
                {
                    Inv.DropItem(itemId);
                    Inv.actionPanel.SetActive(false);
                    Inv.FillInventory("current");
                }
                else
                {
                    int id = Inv.currSelect;
                    Inv.FillInventory("current");
                    Inv.Select(id);
                }
                GameObject.Find("Sounds").GetComponent<Sounds>().PlayBuy();
                break;
            case "Plant":
                Inv.actionPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                if (Inv.currentType == "fertilizer")
                    ToolsClick.currentTool = "fertilizer";
                else
                    ToolsClick.currentTool = "planted";
                Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/cartoon-seeds2"), Vector2.zero, CursorMode.Auto);
                Inv.lockPanelInv.SetActive(false);
                break;
        }
    }
}
