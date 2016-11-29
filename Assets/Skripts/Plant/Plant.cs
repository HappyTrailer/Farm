using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Plant : MonoBehaviour
{
    public int fruitId;
    public double oldTimeFactor; // множитель времени роста 
     
    public int countFruit; // количество плодов
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

    public string stage;   //стадия роста

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
            //======================================================
            //if (stage == "stage1" && currentStage > 0)
            //{
            //    Debug.Log(Time.deltaTime);
            //    currentStage -= Time.deltaTime;

            //    if (currentStage <= 0)
            //    {
            //        currentStage = stageTwo;
            //        stage = "stage2";
            //        shangeSprite("stage2");
            //    }
            //}
            //else if (stage == "stage2" && currentStage > 0)
            //{
            //    currentStage -= Time.deltaTime;

            //    if (currentStage <= 0)
            //    {
            //        currentStage = stageThree;
            //        stage = "stage3";
            //        shangeSprite("stage3");
            //    }
            //}
            //else if (stage == "stage3" && currentStage > 0)
            //{
            //    currentStage -= Time.deltaTime;

            //    if (currentStage <= 0)
            //    {
            //        currentStage = stageFour;
            //        stage = "stage4";
            //        shangeSprite("stage4");
            //    }
            //}
            //else if (stage == "stage4" && currentStage > 0)
            //{
            //    currentStage -= Time.deltaTime;
            //    if (currentStage <= 0)
            //    {
            //        currentStage = 0;
            //        stage = "stage5"; 
            //        shangeSprite("stage5");
            //        growing = false;
            //    }
            //}
            //======================================================
        }
    }

    public void Init(Sead sead)
    {
        XmlDocument seadXml = new XmlDocument();
        TextAsset bindata  = Resources.Load("XML/Seads") as TextAsset; 
        seadXml.LoadXml(bindata.text);
        XmlElement root = seadXml.DocumentElement;
        foreach(XmlElement item in root)
        {
            if (item.ChildNodes[0].InnerText == sead.Id.ToString())
            {
                countFruit = Convert.ToInt32(item.ChildNodes[4].InnerText);
                iterationFruit = Convert.ToInt32(item.ChildNodes[5].InnerText);
                countExpiriens = Convert.ToInt32(item.ChildNodes[6].InnerText);

                stageOne = Convert.ToSingle(item.ChildNodes[7].InnerText);
                stageTwo = Convert.ToSingle(item.ChildNodes[8].InnerText);
                stageThree = Convert.ToSingle(item.ChildNodes[9].InnerText);
                stageFour = Convert.ToSingle(item.ChildNodes[10].InnerText);

                pl1 = Resources.Load<Sprite>("Sprite/Plant/" + item.ChildNodes[11].InnerText);
                pl2 = Resources.Load<Sprite>("Sprite/Plant/" + item.ChildNodes[12].InnerText);
                pl3 = Resources.Load<Sprite>("Sprite/Plant/" + item.ChildNodes[13].InnerText);
                pl4 = Resources.Load<Sprite>("Sprite/Plant/" + item.ChildNodes[14].InnerText);
                pl5 = Resources.Load<Sprite>("Sprite/Plant/" + item.ChildNodes[15].InnerText);
                pl6 = Resources.Load<Sprite>("Sprite/Plant/" + item.ChildNodes[16].InnerText);

                fruitId = Convert.ToInt32(item.ChildNodes[17].InnerText);
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
            Inv.GetHarvestToInventory(countFruit, fruitId);
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
