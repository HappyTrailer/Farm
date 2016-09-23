using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Money : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float money;
    public Text moneytxt;

    private Image img;

    void Update()
    {
        moneytxt.text = money.ToString();
    }

    public void AddMoney(float m)
    {
        this.money += m;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
