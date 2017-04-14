using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ExpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image scr;
    public Text txt;
    public Text lvlTxt;
    public Image notify;
    public Text notifyText;

    private float change = 0;

    public static float max;
    public static int countExperience;
    public static int currentCountlvl;
    public static int[] masLvl = new int[] {20,40,80,160,320,480,720,1080,1944,2527,3285,4270,5552,7217,9382,12197,15857,20614,26798,34837,45289,58876,76539,99500};

	void Update ()
    {
        lvlTxt.text = currentCountlvl.ToString();
        scr.fillAmount = countExperience / max;
        txt.text = (countExperience / max * 100).ToString("F2") + "%";
        notifyText.text = countExperience + " / " + max;
        PlayerPrefs.SetFloat("Exp", countExperience);
        PlayerPrefs.SetFloat("Level", currentCountlvl);
	}

    void Start ()
    {
        Application.runInBackground = true;
        currentCountlvl = (int)PlayerPrefs.GetFloat("Level");
        countExperience = (int)PlayerPrefs.GetFloat("Exp");
        max = masLvl[currentCountlvl];
    }

    public static void AddExp(int x)
    {
        countExperience += x;
        countExperience = GotNextLvl();
    }

    static int GotNextLvl()
    {
        if (countExperience < masLvl[currentCountlvl])
            return countExperience;
        countExperience -= masLvl[currentCountlvl];
        currentCountlvl++;
        max = masLvl[currentCountlvl];
        return GotNextLvl();
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
