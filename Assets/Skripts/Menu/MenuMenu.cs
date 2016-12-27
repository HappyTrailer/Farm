using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject cursor;

    void Start()
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.activeSelf)
            {
                settings.SetActive(false);
            }
        }
        cursor.transform.position = new Vector3(Input.mousePosition.x + 5, Input.mousePosition.y - 10, 0);
    }

    public void Setting()
    {
        if(!settings.activeSelf)
        {
            settings.SetActive(true);
            GameObject.Find("Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sounds");
            GameObject.Find("Slider (1)").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music");
        }
        else
        {
            settings.SetActive(false);
        }
    }
    public void Game()
    {
        Application.LoadLevel(1);
    }
    public void NewGame()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Apply()
    {
        settings.SetActive(false);
    }
    public void ChangeVolumeSounds(float v)
    {
        PlayerPrefs.SetFloat("Sounds", v);
    }
    public void ChangeVolumeMusic(float v)
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = v;
        PlayerPrefs.SetFloat("Music", v);
    }
}
