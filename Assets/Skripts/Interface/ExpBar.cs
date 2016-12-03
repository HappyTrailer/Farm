using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ExpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float max;
    public float current;
    public Image scr;
    public Text txt;
    public Image notify;
    public Text notifyText;

    private float change = 0;

	void Update () {
        AnimExp();
        scr.fillAmount = current / max;
        txt.text = (current / max * 100) + "%";
        notifyText.text = current + " / " + max;
	}

    private void AnimExp()
    {
        float color = scr.color.r;
        if (color >= 1)
        {
            change = -0.002f;
        }
        else if (color <= 0.8f)
        {
            change = 0.002f;
        }
        scr.color = new Color(color + change, color + change, color + change, 255);
    }

    public void ChangeMax(float max)
    {
        this.max = max;
    }

    public void AddExp(float exp)
    {
        this.current += exp;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().PlaySoudTool();
        transform.FindChild("level").transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        scr.transform.parent.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        notify.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.FindChild("level").transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        scr.transform.parent.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        notify.gameObject.SetActive(false);
    }
}
