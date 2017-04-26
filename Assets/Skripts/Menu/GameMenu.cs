using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
                ToolsClick.currentTool = "arrow";
                Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/arrow2"), Vector2.zero, CursorMode.Auto);
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
        Inv.lockPanel.SetActive(false);
        menu.SetActive(false);
    }
    public void NewGame()
    {
        Save s = new Save();
        s.MySave();
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
