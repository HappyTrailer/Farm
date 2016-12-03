using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class FilterPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        Inv.actionPanel.SetActive(false);
        switch (this.name)
        {
            case "Tool":
                Inv.FillInventory("tool");
                break;
            case "Sead":
                Inv.FillInventory("sead");
                break;
            case "Fruit":
                Inv.FillInventory("harvest");
                break;
        }
    }
}
