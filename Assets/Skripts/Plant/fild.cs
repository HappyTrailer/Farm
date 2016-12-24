using UnityEngine;
using System.Collections;

public class fild : MonoBehaviour
{
    public int idFild;

    Sprite digedField;
    Sprite sandField;

    GameObject verminSprite;
    GameObject weedSprite;
    GameObject Sounds;
    

    byte havings;             // принадлежность
    bool dig;                 // вскопаность
    public bool watering;     //политость
    bool weed;                //сорняки
    bool fertilizer;          // удобрено или нет
    double fertilizer_factor; // множитель удобрения
    bool vermin;  //вредители
    public bool sown = false;    //засеяность
    bool hand;    //сбор
    bool inventory;//инвентарь
    bool arrow;    //курсор


    float wateringTime = 10.0f;
    float timeFactor = 1.0f;
    Plant plant;        // тут поле с класом растения
    ListFildEvent events = new ListFildEvent();


    public void OnMouseOver()//тут метод появления времени роста при попадании мышки с значением оставшегося времени
    {
        //if (plant)
            //Debug.Log(plant.AllStage);
            //Debug.Log(ToolsClick.currentTool);
    }
    public void OnMouseExit()
    {
        if (GetComponent<SpriteRenderer>().sortingOrder > 1000)
            GetComponent<SpriteRenderer>().sortingOrder -= 1000;
    }
    public void OnMouseEnter()
    {
        if (sown)
            GetComponent<SpriteRenderer>().sortingOrder += 1000;
    }
    public void OnMouseUp()
    {
        GetMouseValue();
    }
    public void ChangeFert(bool val,float tf)
    {
        //Debug.Log("fert = " +  val);
        if (val)
            timeFactor -= tf;
        else
            timeFactor += tf;
        fertilizer = val;
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
        verminSprite = transform.FindChild("Vermin").gameObject;
        weedSprite = transform.FindChild("Weed").gameObject;
        digedField = Resources.Load<Sprite>("Sprite/Bad/digedField");
        sandField = Resources.Load<Sprite>("Sprite/Bad/sandField");

    }
    void Update()   // В методе апдейт происходит просчет роста растения
    {
        transform.Find("plantSprite").GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        verminSprite.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 3;
        weedSprite.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 2;
        if (plant != null)  // если растение существует продолжать просчет роста
        {
            plant.GrowingProces(timeFactor);  // метод просчета роста
            //Debug.Log(timeFactor +"            "+ Time.deltaTime);
        }
    }

    void GetMouseValue()                   // метод обработки клика мышкой по полю в зависимости от типа курсора
    {
        switch (ToolsClick.currentTool)
        {
            //==========================================================
            case "dig":
                if (!dig)
                {
                    if (plant != null)
                    {
                        if (plant.stage == "stage6")
                        {
                            sown = false;
                            GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudDiging();

                            plant.EndGrow();
                            plant = null;
                            dig = true;
                            this.GetComponent<SpriteRenderer>().sprite = sandField;
                            ChangeWeed(false);
                            ChangeVermin(false);
                            ChangeFert(false, 0.0f);
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
                    GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudWatering();
                    events.AddFildEvent(idFild, 300.0f, "watering");
                    ChangeWatering(false);                    
                }  
                break;
            //==========================================================
            case "weed":
                if (weed)
                {
                    GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudSprey();
                    ChangeWeed(!weed);
                }              
                break;
            //==========================================================
            case "fertilizer":                 // доработать
                if (!fertilizer && sown)
                {
                    Inv.items[Inv.currentFert.ItemId].ItemCount -= 1;

                    if (Inv.items[Inv.currentFert.ItemId].ItemCount <= 0)
                    {
                        ToolsClick.currentTool = "arrow";
                        ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow");
                        Inv.DropItem(Inv.currentFert.ItemId);
                    }
                    ChangeFert(true, Inv.currentFert.timeFactor);
                }
                break;
            //==========================================================
            case "vermin":
                if (vermin)
                {
                    GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudSprey();
                    ChangeVermin(!vermin);
                }
                break;
            //==========================================================
            case "hand":
                if (plant != null)
                {
                    GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudGet();
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
                    sown = true;
                    dig = false;
                    Inv.items[Inv.currentSead.ItemId].ItemCount -= 1;

                    if (Inv.items[Inv.currentSead.ItemId].ItemCount <= 0)
                    {
                        ToolsClick.currentTool = "arrow";
                        ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow");
                        Inv.DropItem(Inv.currentSead.ItemId);
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
