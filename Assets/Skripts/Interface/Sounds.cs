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
    public GameObject camera;

    public void PlayFail()
    {
        AudioSource.PlayClipAtPoint(clipFail, camera.transform.position);
    }
    public void PlaySoudWatering()
    {
        AudioSource.PlayClipAtPoint(clipWatering, camera.transform.position);
    }
    public void PlayBuy()
    {
        AudioSource.PlayClipAtPoint(clipBuy, camera.transform.position);
    }
    public void PlaySoudDiging()
    {
        AudioSource.PlayClipAtPoint(clipDiging, camera.transform.position);
    }
    public void PlaySoudSprey()
    {
        AudioSource.PlayClipAtPoint(clipSprey, camera.transform.position);
    }
    public void PlaySoudGet()
    {
        AudioSource.PlayClipAtPoint(clipGet, camera.transform.position);
    }
    public void PlaySoudTool()
    {
        AudioSource.PlayClipAtPoint(clipTool, camera.transform.position);
    }

}
