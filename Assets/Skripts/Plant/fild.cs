using UnityEngine;
using System.Collections;

public class fild : MonoBehaviour
{
    public Sprite digedField;
    public Sprite sandField;
    /*1.По отношению к  игроку.
        1.1. Грядка приобретенная игроком
        1.2. Не приобретенная грядка
        1.3. Грядка другого игрока
    */
    byte havings;             // принадлежность
    bool dig;                 // вскопаность
    bool watering;               //политость
    byte weed;                 //сорняки
    bool fertilizer;           // удобрено или нет
    double fertilizer_factor; // множитель удобрения
    byte vermin;  //вредители
    bool sown;    //засеяность
    bool hand;    //сбор
    bool inventory;    //инвентарь
    bool arrow;    //курсор


    Plant plant;        // тут поле с класом растения
    public void OnMouseUp()
    {
        GetMouseValue();
    }

    void Update()
    {
        if (plant != null)
        {
            plant.GrowingProces();
        }
    }

    void GetMouseValue()
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
                    watering = true;
                    this.GetComponent<SpriteRenderer>().sprite = digedField;
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
                if (vermin > 0)
                    vermin -= 1;
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
                    ToolCoice.currentTool = "arrow";
                    dig = false;
                    Debug.Log("planted ");
                    this.GetComponentInChildren<Plant>().Init(Inv.currentSead);
                    plant = this.GetComponentInChildren<Plant>();
                    plant.StartGrowing();
                }
                break;
        }
    }
}
