using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class fild : MonoBehaviour
{
    public int idFild;

    public Sprite digedField;
    public Sprite sandField;
    public Sprite lockedSprite;

    GameObject verminSprite;
    GameObject weedSprite;
    GameObject Sounds;
    
    public bool locked;
    bool dig;                 // вскопаность
    public bool watering;     //политость
    bool weed;                //сорняки
    bool fertilizer;          // удобрено или нет
    bool vermin;  //вредители
    public bool sown;    //засеяность

    public static int nextFild;

    float wateringTime = 10.0f;
    float timeFactor = 1.0f;
    Plant plant;        // тут поле с класом растения
    ListFildEvent events = new ListFildEvent();

    public void Load(SaveField f)
    {
        plant = new Plant();
        idFild = f.idFild;
        locked = f.locked;
        dig = f.dig;
        watering = f.watering;
        weed = f.weed;
        fertilizer = f.fertilizer;
        vermin = f.vermin;
        sown = f.sown;
        wateringTime = f.wateringTime;
        timeFactor = f.timeFactor;
        if (f.plant == null)
            plant = null;
        else
        {
            this.GetComponentInChildren<Plant>().Load(f.plant);
            this.GetComponentInChildren<Plant>().shangeSprite(f.plant.stage);
            plant = this.GetComponentInChildren<Plant>();
            if (watering)
                GetComponent<SpriteRenderer>().sprite = digedField;
            else
                GetComponent<SpriteRenderer>().sprite = sandField;
        }
    }

    public SaveField Save()
    {
        SaveField a = new SaveField() 
        {
            idFild = this.idFild,
            locked = this.locked,
            dig = this.dig,
            watering = this.watering,
            weed = this.weed,
            fertilizer = this.fertilizer,
            vermin = this.vermin,
            sown = this.sown,
            wateringTime = this.wateringTime,
            timeFactor = this.timeFactor
        };
        if (plant == null)
            a.plant = null;
        else
            a.plant = this.plant.Save();
        return a;
    }

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
        else 
        {
            this.GetComponent<SpriteRenderer>().sprite = digedField;
            watering = !val;
            timeFactor -= 0.2f;
        }
    }
    void Start()
    {
        nextFild = (int)PlayerPrefs.GetFloat("NextFild");
        if (nextFild < 3)
            nextFild = 3;
        verminSprite = transform.FindChild("Vermin").gameObject;
        weedSprite = transform.FindChild("Weed").gameObject;
        lockedSprite = Resources.Load<Sprite>("Sprite/Bad/lockField");
        digedField = Resources.Load<Sprite>("Sprite/Bad/digedField");
        sandField = Resources.Load<Sprite>("Sprite/Bad/sandField");
        if (idFild == nextFild)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        if (locked && idFild >= nextFild)
            GetComponent<SpriteRenderer>().sprite = lockedSprite;
        else
        {
            if(watering)
                GetComponent<SpriteRenderer>().sprite = digedField;
            else
                GetComponent<SpriteRenderer>().sprite = sandField;
            locked = false;
        }

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
        if (!locked)
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
                        GameObject.Find("Sounds").GetComponent<Sounds>().PlayFert();
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
        else
        {
            if(idFild == nextFild)
            {
                Inv.buyFildPanel.SetActive(true);
                Inv.actionPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                Shop.shopPanel.SetActive(false);
                Inv.currFild = this;
                GameObject.Find("PriceFild").GetComponent<Text>().text = (idFild * 10 * 1.5).ToString();
                ToolsClick.currentTool = "arrow";
                ToolsClick.globalCursor.sprite = Resources.Load<Sprite>("Sprite/InstrumentsPanel/arrow");
                Inv.lockPanelInv.SetActive(true);
            }
        }
    }
}
