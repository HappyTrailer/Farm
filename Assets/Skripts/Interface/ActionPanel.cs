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
                Inv.items[Inv.currentHarv.ItemId].ItemCount -= 1;
                if (Inv.items[Inv.currentHarv.ItemId].ItemCount <= 0)
                {
                    Inv.DropItem(Inv.currentHarv.ItemId);
                    Inv.actionPanel.SetActive(false);
                }
                Inv.FillInventory("current");
                break;
            case "Plant":
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                Inv.actionPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                ToolCoice.currentTool = "planted";
                ToolCoice.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/cartoon-seeds");
                break;
        }
    }
}
