using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour
{
    public AudioClip clipWatering;
    public AudioClip clipDiging;
    public AudioClip clipSprey;
    public AudioClip clipGet;
    public GameObject camera;

    public void PlaySoudWatering()
    {
        AudioSource.PlayClipAtPoint(clipWatering, camera.transform.position);
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

}
