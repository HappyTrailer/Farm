using UnityEngine;
using System.Collections;
using System.Xml;
using System;

[Serializable]
public class SavePlant
{
    public int fruitId;
    public string name;
    public double oldTimeFactor; // множитель времени роста 

    public int mincountFruit; // количество плодов
    public int maxcountFruit; // количество плодов
    public int iterationFruit; // количество раз плодоношения
    public int countExpiriens; // количество опыта

    public float stageOne;
    public float stageTwo;
    public float stageThree;
    public float stageFour;

    public float buffStageThree;
    public float buffStageFour;
    //-----------------------------------------------------------------
    public float currentStage;

    public bool planted;   // посжено ли растение
    public bool growing;

    public string stage; 
}
