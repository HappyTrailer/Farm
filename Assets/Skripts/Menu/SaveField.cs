using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

[Serializable]
public class SaveField
{
    public int idFild;
    public bool locked;
    public bool dig;
    public bool watering;
    public bool weed;
    public bool fertilizer;
    public bool vermin;
    public bool sown;
    public float wateringTime;
    public float timeFactor;
    public SavePlant plant;
}
