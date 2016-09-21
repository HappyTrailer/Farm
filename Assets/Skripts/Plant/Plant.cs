using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    public float growthTime; // время роста
    public double timeFactor; // множитель времени роста 
    public int countFruit; // количество плодов
    public int iterationFruit; // количество раз плодоношения
    public int timeBetweenMat; // время между созреваниями
    public int countExpiriens; // количество опыта

    public Sprite pl1; // семена
    public Sprite pl2; // росток
    public Sprite pl3; // росток 2
    public Sprite pl4; // цветение
    public Sprite pl5; // плодоношение
    public Sprite pl6; // сухой стебель

    float TimerGrow;  //переменная таймер роста
    float oneFive;  // время одной стадии роста

    public bool planted;   // посжено ли растение
    bool growing;

    public string stage;   //стадия роста
    int stagetime;

    void Growth()
    {
        stagetime = (int)TimerGrow;
        ChangeStage();

        if (TimerGrow >= 0 && growing)
        {
            TimerGrow -= Time.deltaTime;
        }
    }

    public void EndGrow()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void GrowingProces()
    {
        if (planted)
        {
            Growth();
        }
    }

    void ChangeStage()
    {
        if (stagetime == (int)growthTime)
        {
            if (stage != "stage1")
            {
                stage = "stage1";
                growing = true;
                Debug.Log(stagetime + " stage1");
                shangeSprite("stage1");
            }
        }
        else if (stagetime == (int)growthTime - oneFive)
        {
            if (stage != "stage2")
            {
                stage = "stage2";
                Debug.Log(stagetime + " stage2");
                shangeSprite("stage2");
            }
        }
        else if (stagetime == (int)growthTime - oneFive * 2)
        {
            if (stage != "stage3")
            {
                stage = "stage3";
                Debug.Log(stagetime + " stage3");
                shangeSprite("stage3");
            }
        }
        else if (stagetime == (int)growthTime - oneFive * 3)
        {
            if (stage != "stage4")
            {
                stage = "stage4";
                Debug.Log(stagetime + " stage4");
                shangeSprite("stage4");
            }
        }
        else if (stagetime == (int)growthTime - oneFive * 4)
        {
            if (stage != "stage5")
            {
                Debug.Log(stagetime + " stage5");
                stage = "stage5";
                shangeSprite("stage5");
                growing = false;
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
            Debug.Log("Item drop");

            if (iterationFruit >= 1)
            {
                Debug.Log("new iteration");
                iterationFruit--;
                shangeSprite("stage3");
                TimerGrow = growthTime - oneFive * 2;
                growing = true;
            }
            else
            {
                if (stage != "stage6")
                {
                    TimerGrow = 0;
                    Debug.Log("end");
                    stage = "stage6";
                    Debug.Log(stagetime + " stage6");
                    shangeSprite("stage6");
                }
            }
        }
    }

    public void StartGrowing()
    {
        planted = true;
        oneFive = (growthTime / 5);
        TimerGrow = growthTime;
        stage = "satge1";
    }
}
