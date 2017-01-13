using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject cursor;

    void Start()
    {
        if (!File.Exists(Application.dataPath + "/Saves/ev.sv") || !File.Exists(Application.dataPath + "/Saves/inv.sv") ||
            !File.Exists(Application.dataPath + "/Saves/fild.sv"))
        {
            NewGame();
        }
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
        PlayerPrefs.SetFloat("Exp", 0);
        PlayerPrefs.SetFloat("Level", 0);
        PlayerPrefs.SetFloat("Money", 0);
        PlayerPrefs.SetFloat("NextFild", 0);
        PlayerPrefs.SetFloat("GameExist", 0);
        if (Directory.Exists(Application.dataPath + "/Saves"))
            Directory.Delete(Application.dataPath + "/Saves", true);
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
