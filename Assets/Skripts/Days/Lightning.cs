using UnityEngine;
using System.Collections;
using System;

public class Lightning : MonoBehaviour
{
    public Transform mon;
    public Transform sun;

    public float sec;
    public float min;
    public float hour;

    public float magicHour;

    private Light lg;
    private float currentTime;
    private float oneUnitLight;
    private float oneUnitTime;
    private float currentLight;
    private const float lights = 0.65f;
    private const float total = 43200;
    private const float time = 6.284f;
    private float timeCounter = 0;

    //private float oneSecond = 0;

    void Start()
    {
        lg = GetComponent<Light>();
        oneUnitLight = lights / total;
        oneUnitTime = time / (total * 2);
    }

    void Update()
    {
        //==============HAND===============================
        //oneSecond += Time.deltaTime;
        //if (oneSecond >= 1.0f)
        //{
        //    oneSecond = 0;
        //    currentTime += 1;
        //}
        //if (hour >= 0 && hour < 3)
        //    currentTime = ((24 - 3 + hour) * 60 * 60) + (min * 60) + sec;
        //else
        //    currentTime = ((hour - 3) * 60 * 60) + (min * 60) + sec;
        //==============SYSTEM===============================
        hour = DateTime.Now.Hour;
        min = DateTime.Now.Minute;
        sec = DateTime.Now.Second;
        if (hour >= 0 && hour < magicHour)
            currentTime = ((24 - magicHour + hour) * 60 * 60) + (min * 60) + sec;
        else
            currentTime = ((hour - magicHour) * 60 * 60) + (min * 60) + sec;
        //=================================================
        if (currentTime >= total * 2)
            currentTime = 0;
        if (currentTime >= total)
            currentLight = (total - (currentTime - total)) * oneUnitLight;
        else
            currentLight = currentTime * oneUnitLight;
        lg.intensity = currentLight;

        timeCounter = currentTime * oneUnitTime;

        float x = (float)Math.Sin(timeCounter) * 60;
        float y = (float)Math.Cos(timeCounter) * 25;

        sun.transform.localPosition = new Vector3(-x, -y, 0);
        mon.transform.localPosition = new Vector3(x, y, 0);
    }
}
