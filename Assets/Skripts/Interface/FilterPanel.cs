using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class FilterPanel : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Inv.actionPanel.SetActive(false);
        switch (this.name)
        {
            case "Tool":
                Inv.currentType = "fertilizer";
                Inv.FillInventory("first");
                break;
            case "Sead":
                Inv.currentType = "sead";
                Inv.FillInventory("first");
                break;
            case "Fruit":
                Inv.currentType = "harvest";
                Inv.FillInventory("first");
                break;
        }
    }
}
