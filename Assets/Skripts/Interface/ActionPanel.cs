using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ActionPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
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
                break;
            case "Plant":
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                Inv.actionPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                ToolCoice.currentTool = "planted";
                ToolCoice.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/cartoon-seeds");
                break;
            case "Buy":
                break;
        }
    }
}
