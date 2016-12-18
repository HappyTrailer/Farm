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
            time = 2,
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
        seads.Add(new PlantItem()
        {
            id = 4,
            name = "Капуста",
            level = 10,
            time = 60,
            price = 24,
            priceFruit = 100,
            iterationFruit = 1,
            minCountFruit = 1,
            maxCountFruit = 1,
            countExpiriens = 80
        });
        seads.Add(new PlantItem()
        {
            id = 5,
            name = "Помидор",
            level = 13,
            time = 75,
            price = 30,
            priceFruit = 12,
            iterationFruit = 3,
            minCountFruit = 3,
            maxCountFruit = 5,
            countExpiriens = 130
        });
        seads.Add(new PlantItem()
        {
            id = 6,
            name = "Перец красный",
            level = 16,
            time = 90,
            price = 35,
            priceFruit = 15,
            iterationFruit = 3,
            minCountFruit = 3,
            maxCountFruit = 5,
            countExpiriens = 192
        });
        seads.Add(new PlantItem()
        {
            id = 7,
            name = "Перец  желтый",
            level = 20,
            time = 105,
            price = 40,
            priceFruit = 19,
            iterationFruit = 3,
            minCountFruit = 3,
            maxCountFruit = 5,
            countExpiriens = 280
        });
        seads.Add(new PlantItem()
        {
            id = 8,
            name = "Кукуруза",
            level = 23,
            time = 120,
            price = 50,
            priceFruit = 50,
            iterationFruit = 1,
            minCountFruit = 4,
            maxCountFruit = 7,
            countExpiriens = 368
        });
        seads.Add(new PlantItem()
        {
            id = 9,
            name = "Огурец",
            level = 26,
            time = 135,
            price = 60,
            priceFruit = 22,
            iterationFruit = 4,
            minCountFruit = 3,
            maxCountFruit = 5,
            countExpiriens = 468
        });
        seads.Add(new PlantItem()
        {
            id = 10,
            name = "Кабачек",
            level = 30,
            time = 150,
            price = 70,
            priceFruit = 240,
            iterationFruit = 2,
            minCountFruit = 1,
            maxCountFruit = 1,
            countExpiriens = 600
        });
        seads.Add(new PlantItem()
        {
            id = 11,
            name = "Горох",
            level = 33,
            time = 165,
            price = 25,
            priceFruit = 20,
            iterationFruit = 3,
            minCountFruit = 6,
            maxCountFruit = 10,
            countExpiriens = 726
        });
        seads.Add(new PlantItem()
        {
            id = 12,
            name = "Баклажан",
            level = 36,
            time = 180,
            price = 80,
            priceFruit = 600,
            iterationFruit = 1,
            minCountFruit = 1,
            maxCountFruit = 1,
            countExpiriens = 864
        });
        seads.Add(new PlantItem()
        {
            id = 13,
            name = "Дыня",
            level = 40,
            time = 195,
            price = 50,
            priceFruit = 150,
            iterationFruit = 2,
            minCountFruit = 1,
            maxCountFruit = 3,
            countExpiriens = 1040
        });
        seads.Add(new PlantItem()
        {
            id = 14,
            name = "Арбуз",
            level = 43,
            time = 210,
            price = 60,
            priceFruit = 180,
            iterationFruit = 2,
            minCountFruit =1,
            maxCountFruit = 3,
            countExpiriens = 1204
        });
        seads.Add(new PlantItem()
        {
            id = 15,
            name = "Клубника",
            level = 46,
            time = 225,
            price = 100,
            priceFruit = 33,
            iterationFruit = 4,
            minCountFruit = 6,
            maxCountFruit = 10,
            countExpiriens = 1380
        });
        seads.Add(new PlantItem()
        {
            id = 16,
            name = "Ананас",
            level = 49,
            time = 240,
            price = 200,
            priceFruit = 1500,
            iterationFruit = 1,
            minCountFruit = 1,
            maxCountFruit = 1,
            countExpiriens = 1600
        });
    }
}
