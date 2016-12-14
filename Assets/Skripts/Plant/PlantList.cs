using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlantList : MonoBehaviour {

    public static List<PlantItem> seads;

	void Start () {
        seads = new List<PlantItem>();
        seads.Add(new PlantItem()
        {
            id = 1,
            name = "Репа",
            level = 1,
            time = 15,
            price = 1,
            priceFruit = 3,
            iterationFruit = 1,
            minCountFruit = 1,
            maxCountFruit = 1,
            countExpiriens = 2
        });
        seads.Add(new PlantItem()
        {
            id = 2,
            name = "Морковь",
            level = 3,
            time = 30,
            price = 3,
            priceFruit = 10,
            iterationFruit = 1,
            minCountFruit = 1,
            maxCountFruit = 1,
            countExpiriens = 12
        });
        seads.Add(new PlantItem()
        {
            id = 3,
            name = "Картофель",
            level = 6,
            time = 45,
            price = 12,
            priceFruit = 14,
            iterationFruit = 1,
            minCountFruit = 4,
            maxCountFruit = 7,
            countExpiriens = 36
        });
	}
}
