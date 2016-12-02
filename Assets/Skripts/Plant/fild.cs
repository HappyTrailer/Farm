using UnityEngine;
using System.Collections;

public class fild : MonoBehaviour
{
    public int idFild;

    public Sprite digedField;
    public Sprite sandField;

    public GameObject verminSprite;
    public GameObject weedSprite;
    public GameObject Sounds;
    

    /*1.По отношению к  игроку.
        1.1. Грядка приобретенная игроком
        1.2. Не приобретенная грядка
        1.3. Грядка другого игрока
    */
    byte havings;             // принадлежность
    bool dig;                 // вскопаность
    public bool watering;               //политость
    bool weed;                 //сорняки
    bool fertilizer;           // удобрено или нет
    double fertilizer_factor; // множитель удобрения
    bool vermin;  //вредители
    bool sown;    //засеяность
    bool hand;    //сбор
    bool inventory;    //инвентарь
    bool arrow;    //курсор


    float wateringTime = 10.0f;
    float timeFactor = 1.0f;
    Plant plant;        // тут поле с класом растения
    ListFildEvent events = new ListFildEvent();


    void OnMouseOver()//тут метод появления времени роста при попадании мышки с значением оставшегося времени
    {
        if(plant)
        Debug.Log(plant.AllStage);
    }
    public void OnMouseUp()
    {
        GetMouseValue();
    }
    public void ChangeWeed(bool val)
    {

        weedSprite.SetActive(val);
        if (!val)
            timeFactor -= 0.2f;
        else
            timeFactor += 0.2f;
        weed = val;
    }
    public void ChangeVermin(bool val)
    {

        verminSprite.SetActive(val);
        if (!val)
            timeFactor -= 0.2f;
        else
            timeFactor += 0.2f;
        vermin = val;
    }
    public void ChangeWatering(bool val)
    {

        if (val)
        {
            this.GetComponent<SpriteRenderer>().sprite = sandField;
            watering = !val;
            Debug.Log(watering);
            timeFactor += 0.2f;
        }
        else {
            
            this.GetComponent<SpriteRenderer>().sprite = digedField;
            watering = !val;
            Debug.Log(watering);
            timeFactor -= 0.2f;
        }
    }
    void Start()
    {

    }
    void Update()   // В методе апдейт происходит просчет роста растения
    {
        
        if (plant != null)  // если растение существует продолжать просчет роста
        {
            plant.GrowingProces(timeFactor);  // метод просчета роста
            //Debug.Log(timeFactor +"            "+ Time.deltaTime);
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
                            Sounds.GetComponent<Sounds>().PlaySoudDiging();

                            plant.EndGrow();
                            plant = null;
                            dig = true;
                            this.GetComponent<SpriteRenderer>().sprite = sandField;
                            ChangeWeed(false);
                            ChangeVermin(false);
                            watering = true;
                            ChangeWatering(true);
                            timeFactor = 1.0f;
                        }
                    }
                }
                break;
            //==========================================================
            case "watering":
                
                if (!watering)
                {
                    Sounds.GetComponent<Sounds>().PlaySoudWatering();
                    events.AddFildEvent(idFild, 300.0f, "watering");
                    ChangeWatering(false);
                    
                }
                
      
                break;
            //==========================================================
            case "weed":
                if (weed)
                {
                    Sounds.GetComponent<Sounds>().PlaySoudSprey();
                    ChangeWeed(!weed);
                }
                
                break;
            //==========================================================
            case "fertilizer":
                if (!fertilizer)
                    fertilizer = true;
                break;
            //==========================================================
            case "vermin":
                if (vermin)
                {
                    Sounds.GetComponent<Sounds>().PlaySoudSprey();
                    ChangeVermin(!vermin);
                }
                break;
            //==========================================================
            case "hand":
                if (plant != null)
                {
                    Sounds.GetComponent<Sounds>().PlaySoudGet();
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
                    Inv.items[Inv.currSelect + Inv.counter].ItemCount -= 1;

                    if (Inv.items[Inv.currSelect + Inv.counter].ItemCount <= 0)
                    {
                        ToolCoice.currentTool = "arrow";
                        ToolCoice.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow_cartoon_blue_left 1");
                        Inv.DropItem(Inv.currSelect + Inv.counter);
                    }

                    this.GetComponentInChildren<Plant>().Init(Inv.currentSead);
                    plant = this.GetComponentInChildren<Plant>();
                    plant.StartGrowing(timeFactor);
                    events.GenEvent(idFild, plant.GrowingTime, "vermin");
                    events.GenEvent(idFild, plant.GrowingTime, "weed");
                }
                break;
        }
    }


   
}
