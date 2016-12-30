using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public GameObject menu;
    public GameObject settings;
    public AudioClip clipChek;

    void Start()
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Inv.buyFildPanel.activeSelf)
            {
                Inv.buyFildPanel.SetActive(false);
            }
            else if (Inv.inventoryPanel.activeSelf)
            {
                Inv.actionPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
            }
            else if (Shop.shopPanel.activeSelf)
            {
                Shop.shopPanel.SetActive(false);
            }
            else if (settings.activeSelf)
            {
                settings.SetActive(false);
                menu.SetActive(true);
            }
            else if (!menu.activeSelf)
            {
                menu.SetActive(true);
            }
            else if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
        }
    }

	public void Setting()
    {
        menu.SetActive(false);
        settings.SetActive(true);
        GameObject.Find("Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sounds");
        GameObject.Find("Slider (1)").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music");
    }
    public void Game()
    {
        menu.SetActive(false);
    }
    public void Save()
    {

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
        menu.SetActive(true);
    }
    public void ChangeVolumeSounds(float v)
    {
        PlayerPrefs.SetFloat("Sounds", v);
        AudioSource.PlayClipAtPoint(clipChek, Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds"));
    }
    public void ChangeVolumeMusic(float v)
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = v;
        PlayerPrefs.SetFloat("Music", v);
    }
}
