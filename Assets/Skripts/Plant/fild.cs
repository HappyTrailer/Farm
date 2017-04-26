using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class fild : MonoBehaviour
{
    public int idFild;

    public static Sprite digedField;
    public static Sprite sandField;
    public static Sprite lockedSprite;
    
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

    bool hover = false;

    public void Load(SaveField f)
    {
        plant = new Plant();
        idFild = f.idFild;
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
            if (weed)
                transform.Find("Weed").gameObject.SetActive(true);
            if (vermin)
                transform.Find("Vermin").gameObject.SetActive(true);
        }
        if (idFild < nextFild)
        {
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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (plant)
            {
                if (hover == false)
                {
                    Inv.notifyPanel.SetActive(true);
                    Inv.notifyPanel.transform.position = Input.mousePosition + new Vector3(0f, 100f, 0f);
                    hover = true;
                }
                if (plant.AllStage > 0)
                    Inv.notifyPanel.transform.GetChild(0).GetComponent<Text>().text = (int)(plant.AllStage / 60) + " мин. " + (int)(plant.AllStage % 60) + " сек.";
                else
                    Inv.notifyPanel.transform.GetChild(0).GetComponent<Text>().text = "Готово к сбору!";
            }
            GetComponent<Renderer>().material.SetFloat("_OutlineOffSet", 10f);
            if (GetComponent<SpriteRenderer>().sortingOrder < 1000)
                GetComponent<SpriteRenderer>().sortingOrder += 1000;
        }
        else
        {
            Inv.notifyPanel.SetActive(false);
            GetComponent<Renderer>().material.SetFloat("_OutlineOffSet", 0f);
            hover = false;
        }
    }
    public void OnMouseExit()
    {
        Inv.notifyPanel.SetActive(false);
        GetComponent<Renderer>().material.SetFloat("_OutlineOffSet", 0f);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (GetComponent<SpriteRenderer>().sortingOrder > 1000)
            {
                GetComponent<SpriteRenderer>().sortingOrder -= 1000;
                hover = false;
            }
        }
        Inv.notifyPanel.SetActive(false);
    }
    public void OnMouseUp()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            GetMouseValue();
    }
    public void ChangeFert(bool val,float tf)
    {
        if (val)
            timeFactor -= tf;
        else
            timeFactor += tf;
        fertilizer = val;
    }
    public void ChangeWeed(bool val)
    {
        transform.Find("Weed").gameObject.SetActive(val);
        if (!val)
            timeFactor -= 0.2f;
        else
            timeFactor += 0.2f;
        weed = val;
    }
    public void ChangeVermin(bool val)
    {
        transform.Find("Vermin").gameObject.SetActive(val);
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
        lockedSprite = Resources.Load<Sprite>("Sprite/Bad/lockField");
        digedField = Resources.Load<Sprite>("Sprite/Bad/digedField");
        sandField = Resources.Load<Sprite>("Sprite/Bad/sandField");
        nextFild = (int)PlayerPrefs.GetFloat("NextFild");
        if (nextFild < 3)
            nextFild = 3;
        if (idFild == nextFild)
            transform.GetChild(3).gameObject.SetActive(true);
        if (idFild >= nextFild)
            GetComponent<SpriteRenderer>().sprite = lockedSprite;
    }
    void Update()   // В методе апдейт происходит просчет роста растения
    {
        transform.Find("SaleSprite").GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        if (plant != null)  // если растение существует продолжать просчет роста
        {
            transform.Find("plantSprite").GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
            transform.Find("Vermin").GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 3;
            transform.Find("Weed").GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 2;
            plant.GrowingProces(timeFactor);  // метод просчета роста
            //Debug.Log(timeFactor +"            "+ Time.deltaTime);
        }
    }

    void GetMouseValue()                   // метод обработки клика мышкой по полю в зависимости от типа курсора
    {
        if (idFild < nextFild)
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
                            Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/arrow2"), Vector2.zero, CursorMode.Auto);
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
                            Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/arrow2"), Vector2.zero, CursorMode.Auto);
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
            if (idFild == nextFild)
            {
                Inv.buyFildPanel.SetActive(true);
                Inv.actionPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                Shop.shopPanel.SetActive(false);
                Inv.currFild = this;
                GameObject.Find("PriceFild").GetComponent<Text>().text = (idFild * 10 * 1.5).ToString();
                ToolsClick.currentTool = "arrow";
                Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/arrow2"), Vector2.zero, CursorMode.Auto);
                Inv.lockPanelInv.SetActive(true);
            }
        }
    }
}
