using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ActionPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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
        switch(this.name)
        {
            case "Sell":
                break;
            case "Plant":
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                Inv.actionPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                ToolCoice.currentTool = "planted";
                ToolCoice.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/cartoon-seeds");
                break;
        }
    }
}
