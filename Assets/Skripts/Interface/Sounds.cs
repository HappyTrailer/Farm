using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour
{
    public AudioClip clipWatering;
    public AudioClip clipDiging;
    public AudioClip clipSprey;
    public AudioClip clipGet;
    public AudioClip clipTool;
    public AudioClip clipBuy;
    public AudioClip clipFail;
    public AudioClip clipFert;
    public GameObject camera;

    public void PlayFert()
    {
        AudioSource.PlayClipAtPoint(clipFert, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void PlayFail()
    {
        AudioSource.PlayClipAtPoint(clipFail, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void PlaySoudWatering()
    {
        AudioSource.PlayClipAtPoint(clipWatering, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void PlayBuy()
    {
        AudioSource.PlayClipAtPoint(clipBuy, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void PlaySoudDiging()
    {
        AudioSource.PlayClipAtPoint(clipDiging, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void PlaySoudSprey()
    {
        AudioSource.PlayClipAtPoint(clipSprey, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void PlaySoudGet()
    {
        AudioSource.PlayClipAtPoint(clipGet, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void PlaySoudTool()
    {
        AudioSource.PlayClipAtPoint(clipTool, camera.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
}
