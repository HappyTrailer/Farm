using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour
{
    public float money;
    public float dalars;
    public Text moneytxt;
    public Text dolarstxt;

    void Update()
    {
        moneytxt.text = money.ToString();
        dolarstxt.text = dalars.ToString();
    }

    public void AddMoney(float m)
    {
        this.money += m;
    }

    public void AddDolars(float d)
    {
        this.dalars += d;
    }
}
