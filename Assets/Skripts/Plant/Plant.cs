using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    public double timeFactor; // множитель времени роста 
    public int countFruit; // количество плодов
    public int iterationFruit; // количество раз плодоношения
    public int countExpiriens; // количество опыта

    public float stageOne;
    public float stageTwo;
    public float stageThree;
    public float stageFour;

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

    public void GrowingProces()
    {
        if (planted && growing)
        {
            if (stage == "stage1" && currentStage > 0)
            {
                currentStage -= Time.deltaTime;
                if (currentStage <= 0)
                {
                    currentStage = stageTwo;
                    stage = "stage2";
                    shangeSprite("stage2");
                }
            }
            else if (stage == "stage2" && currentStage > 0)
            {
                currentStage -= Time.deltaTime;
                if (currentStage <= 0)
                {
                    currentStage = stageThree;
                    stage = "stage3";
                    shangeSprite("stage3");
                }
            }
            else if (stage == "stage3" && currentStage > 0)
            {
                currentStage -= Time.deltaTime;
                if (currentStage <= 0)
                {
                    currentStage = stageFour;
                    stage = "stage4";
                    shangeSprite("stage4");
                }
            }
            else if (stage == "stage4" && currentStage > 0)
            {
                currentStage -= Time.deltaTime;
                if (currentStage <= 0)
                {
                    currentStage = 0;
                    stage = "stage5"; 
                    shangeSprite("stage5");
                    growing = false;
                }
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
            if (iterationFruit >= 1)
            {
                iterationFruit--;
                stage = "stage3";
                shangeSprite("stage3");
                currentStage = stageThree;
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

    public void StartGrowing()
    {
        planted = true;
        growing = true;
        currentStage = stageOne;
        stage = "stage1";
        shangeSprite("stage1");
    }
}
