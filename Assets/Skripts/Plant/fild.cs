﻿using UnityEngine;
using System.Collections;

public class fild : MonoBehaviour
{
    public int idFild;

    public Sprite digedField;
    public Sprite sandField;
    /*1.По отношению к  игроку.
        1.1. Грядка приобретенная игроком
        1.2. Не приобретенная грядка
        1.3. Грядка другого игрока
    */
    byte havings;             // принадлежность
    bool dig;                 // вскопаность
    public bool watering;               //политость
    byte weed;                 //сорняки
    bool fertilizer;           // удобрено или нет
    double fertilizer_factor; // множитель удобрения
    bool vermin;  //вредители
    bool sown;    //засеяность
    bool hand;    //сбор
    bool inventory;    //инвентарь
    bool arrow;    //курсор


    float wateringTime = 10.0f;
    Plant plant;        // тут поле с класом растения
    ListFildEvent events = new ListFildEvent();
    public void OnMouseUp()
    {
        GetMouseValue();
    }
    public void ChangeVermin()
    {

        if (vermin)
        {
            this.GetComponent<SpriteRenderer>().sprite = sandField;
            vermin = !vermin;

        }
        else
        {

            this.GetComponent<SpriteRenderer>().sprite = digedField;
            vermin = !vermin;
        }
    }
    public void ChangeWatering()
    {

        if (watering)
        {
            this.GetComponent<SpriteRenderer>().sprite = sandField;
            watering = !watering;
            
        }
        else {
            
            this.GetComponent<SpriteRenderer>().sprite = digedField;
            watering = !watering;
        }
    }
    void Update()   // В методе апдейт происходит просчет роста растения
    {
        
        if (plant != null)  // если растение существует продолжать просчет роста
        {
            plant.GrowingProces();  // метод просчета роста
        }
    }

    void GetMouseValue()                   // метод обработки клика мышкой по полю в зависимости от типа курсора
    {
        switch (ToolCoice.currentTool)
        {
            //==========================================================
            case "dig":
                if (!dig)
                {
                    if (plant != null)
                    {
                        if (plant.stage == "stage6")
                        {
                            plant.EndGrow();
                            plant = null;
                            dig = true;
                            this.GetComponent<SpriteRenderer>().sprite = sandField;
                        }
                    }
                }
                break;
            //==========================================================
            case "watering":
                
                if (!watering)
                {
                    events.AddFildEvent(idFild, 5.0f, "watering");
                    ChangeWatering();
                }
                
      
                break;
            //==========================================================
            case "weed":
                if (weed > 0)
                    weed -= 1;
                break;
            //==========================================================
            case "fertilizer":
                if (!fertilizer)
                    fertilizer = true;
                break;
            //==========================================================
            case "vermin":
                
                break;
            //==========================================================
            case "hand":
                if (plant != null)
                {
                    plant.DropFruit();
                }
                break;
            //==========================================================
            case "inventory":
                break;
            //==========================================================
            case "arrow":
                break;
            //==========================================================
            case "sown":
                break;
            //==========================================================
            case "planted":
                if (plant == null)
                {
                    dig = false;
                   // Debug.Log("planted ");
                    Inv.items[Inv.currSelect].ItemCount -= 1;

                    if (Inv.items[Inv.currSelect].ItemCount <= 0)
                    {
                        ToolCoice.currentTool = "arrow";
                        ToolCoice.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow_cartoon_blue_left 1");
                        Inv.DropItem(Inv.currSelect);
                    }

                    this.GetComponentInChildren<Plant>().Init(Inv.currentSead);
                    plant = this.GetComponentInChildren<Plant>();
                    plant.StartGrowing();
                }
                break;
        }
    }
   
}
