using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocInvAndShop : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.name == "LockMenu")
        {
            Inv.lockPanel.SetActive(false);
            GameMenu.settingsPanel.SetActive(false);
            GameMenu.menuPanel.SetActive(false);
        }
        else if(this.name == "LockInv")
        {
            Inv.actionPanel.SetActive(false);
            Inv.filterPanel.SetActive(false);
            Inv.inventoryPanel.SetActive(false);
            Inv.inventoryPanel.SetActive(false);
            Shop.shopPanel.SetActive(false);
            Inv.lockPanelInv.SetActive(false);
        }
    }
}
