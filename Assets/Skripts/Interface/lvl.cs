using UnityEngine;
using System.Collections;

public class lvl {

    public static int currentCountlvl;
    public static int countExperience;
    public static int[] masLvl = new int[] {3, 8,80,160,320,480,720,1080,1944,2527,3285,4270,5552,7217,9382,12197,15857,20614,26798,34837,
   45289,58876,76539,99500};

    public static void AddExp(int x)
    {
        countExperience += x;
        if (countExperience >= masLvl[currentCountlvl])
        {
            countExperience = countExperience - masLvl[currentCountlvl];
            currentCountlvl++;

            ExpBar.max = masLvl[currentCountlvl];
            ExpBar.current = countExperience;
        }
    }
   
}
