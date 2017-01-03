using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class GameMenu : MonoBehaviour {

    public GameObject menu;
    public GameObject settings;
    public AudioClip clipChek;

    public static GameObject menuPanel;
    public static GameObject settingsPanel;

    void Start()
    {
        menuPanel = menu;
        settingsPanel = settings;
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Inv.buyFildPanel.activeSelf)
            {
                Inv.buyFildPanel.SetActive(false);
                Inv.lockPanelInv.SetActive(false);
            }
            else if (Inv.inventoryPanel.activeSelf)
            {
                Inv.actionPanel.SetActive(false);
                Inv.filterPanel.SetActive(false);
                Inv.inventoryPanel.SetActive(false);
                Inv.lockPanelInv.SetActive(false);
            }
            else if (Shop.shopPanel.activeSelf)
            {
                Shop.shopPanel.SetActive(false);
                Inv.lockPanelInv.SetActive(false);
            }
            else if (settings.activeSelf)
            {
                settings.SetActive(false);
                menu.SetActive(true);
            }
            else if (!menu.activeSelf)
            {
                Inv.lockPanel.SetActive(true);
                menu.SetActive(true);
            }
            else if (menu.activeSelf)
            {
                Inv.lockPanel.SetActive(false);
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
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        if (Directory.Exists(Application.dataPath + "/Saves"))
            Directory.Delete(Application.dataPath + "/Saves", true);
        Application.LoadLevel(0);
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
