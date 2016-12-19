using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Shop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject shopP;
    private static GameObject shopPanel;

    void Start()
    {
        shopPanel = shopP;
    }

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
        Inv.actionPanel.SetActive(false);
        Inv.filterPanel.SetActive(false);
        Inv.inventoryPanel.SetActive(false);
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
