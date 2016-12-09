using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Money : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static float money;
    public static float donate;

    public Text moneytxt;

    private Image img;

    void Update()
    {
        switch (this.name)
        {
            case "money":
                moneytxt.text = money.ToString();
                break;
            case "doante":
                moneytxt.text = donate.ToString();
                break;
        }
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
