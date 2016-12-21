using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Plant : MonoBehaviour
{
    public int fruitId;
    public double oldTimeFactor; // множитель времени роста 

    public int mincountFruit; // количество плодов
    public int maxcountFruit; // количество плодов
    public int iterationFruit; // количество раз плодоношения
    public int countExpiriens; // количество опыта

    //*************************Стадии роста по времени*****************
    public float stageOne;
    public float stageTwo;
    public float stageThree;
    public float stageFour;

    public float buffStageThree;
    public float buffStageFour;
    //-----------------------------------------------------------------
    private float currentStage;

    public Sprite pl1; // семена
    public Sprite pl2; // росток
    public Sprite pl3; // росток 2
    public Sprite pl4; // цветение
    public Sprite pl5; // плодоношение
    public Sprite pl6; // сухой стебель

    public bool planted;   // посжено ли растение
    bool growing;

    public string stage;   //текущая стадия роста

    public void EndGrow()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
    }
    public float AllStage
    {
        get {

            return stageOne + stageTwo + stageThree + stageFour;
        }
    }
    public void FixStageByTimeFactor(float t)
    {
        if (oldTimeFactor != t)
        {
            stageOne *= t;
            stageTwo *= t;
            stageThree *= t;
            stageFour *= t;
            oldTimeFactor = t;
           // Debug.Log(stageOne + " " + stageTwo + " " + stageThree + " " + stageFour + " " + t );
        }
    }
    public void GrowingProces(float timeFactor)
    {
        if (planted && growing)
        {
            if (AllStage >= 0)
            {
                //  Debug.Log(AllStage);
                FixStageByTimeFactor(timeFactor);
                switch (stage)
                {
                    case "stage1":
                        stageOne -= Time.deltaTime;
                        if (stageOne <= 0)
                        {
                            // currentStage = stageTwo;
                            stage = "stage2";
                            shangeSprite("stage2");
                            stageOne = 0;
                        }
                        break;
                    case "stage2":
                        stageTwo -= Time.deltaTime;
                        if (stageTwo <= 0)
                        {
                            // currentStage = stageTwo;
                            stage = "stage3";
                            shangeSprite("stage3");
                            stageTwo = 0;
                        }
                        break;
                    case "stage3":
                        stageThree -= Time.deltaTime;

                        if (stageThree <= 0)
                        {
                            stage = "stage4";
                            shangeSprite("stage4");
                            stageThree = 0;
                        }
                        break;
                    case "stage4":
                        stageFour -= Time.deltaTime;

                        if (stageFour <= 0)
                        {
                            stage = "stage5";
                            shangeSprite("stage5");
                            growing = false;
                            stageFour = 0;

                        }
                        break;
                }
            }
           
        }
    }

    public void Init(Sead sead)
    {
        foreach(PlantItem item in PlantList.seads)
        {
            if (item.id == sead.Id)
            {
                maxcountFruit = item.maxCountFruit;
                mincountFruit = item.minCountFruit;
                iterationFruit = item.iterationFruit - 1;
                countExpiriens = item.countExpiriens;

                stageOne = item.time;
                stageTwo = item.time;
                stageThree = item.time;
                stageFour = item.time;

                pl1 = Resources.Load<Sprite>("Sprite/Plant/" + item.name + "/pl1");
                pl2 = Resources.Load<Sprite>("Sprite/Plant/" + item.name + "/pl2");
                pl3 = Resources.Load<Sprite>("Sprite/Plant/" + item.name + "/pl3");
                pl4 = Resources.Load<Sprite>("Sprite/Plant/" + item.name + "/pl4");
                pl5 = Resources.Load<Sprite>("Sprite/Plant/" + item.name + "/pl5");
                pl6 = Resources.Load<Sprite>("Sprite/Plant/" + item.name + "/pl6");

                fruitId = item.id;
            }
        }
    }

    void shangeSprite(string s)
    {
        switch (s)
        {
            case "stage1":
                this.GetComponent<SpriteRenderer>().sprite = pl1;
                break;
            case "stage2":
                this.GetComponent<SpriteRenderer>().sprite = pl2;
                break;
            case "stage3":
                this.GetComponent<SpriteRenderer>().sprite = pl3;
                break;
            case "stage4":
                this.GetComponent<SpriteRenderer>().sprite = pl4;
                break;
            case "stage5":
                this.GetComponent<SpriteRenderer>().sprite = pl5;
                break;
            case "stage6":
                this.GetComponent<SpriteRenderer>().sprite = pl6;
                break;
        }
    }

    public void DropFruit()
    {
        if (stage == "stage5")
        {
            int countEv = UnityEngine.Random.Range(mincountFruit, maxcountFruit);
            Inv.GetHarvestToInventory(countEv, fruitId, "harvest");
            lvl.AddExp(countExpiriens);
            ExpBar.current = lvl.countExperience;
            if (iterationFruit >= 1)
            {
                iterationFruit--;
                stage = "stage3";
                shangeSprite("stage3");
                stageThree = buffStageThree;
                stageFour = buffStageFour;
                growing = true;
            }
            else
            {
                if (stage != "stage6")
                {
                    stage = "stage6";
                    shangeSprite("stage6");
                }
            }
        }
    }

    public void StartGrowing(float timeFactor)
    {
        buffStageThree = stageThree;
        buffStageFour = stageFour;
        oldTimeFactor = timeFactor;
        planted = true;
        growing = true;
        FixStageByTimeFactor(timeFactor);
        // allStage = stageOne + stageTwo + stageThree + stageFour;
        stage = "stage1";
        shangeSprite("stage1");
    }

    public float GrowingTime
    {
        get { return stageOne + stageTwo + stageThree + stageFour;}
    }
}
