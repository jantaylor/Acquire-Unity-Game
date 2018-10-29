using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject LocalPlayMenu;
    public GameObject OnlinePlayMenu;
    public GameObject HowToPlayMenu;

    private void Awake() {
        MainMenu = GameObject.Find("MainMenu");
        OptionsMenu = GameObject.Find("OptionsMenu");
        LocalPlayMenu = GameObject.Find("LocalPlayMenu");
        OnlinePlayMenu = GameObject.Find("OnlinePlayMenu");
        HowToPlayMenu = GameObject.Find("HowToPlayMenu");
    }

    private void Start() {
        HideAllMenus();
    }

    public void HideAllMenus() {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        LocalPlayMenu.SetActive(false);
        OnlinePlayMenu.SetActive(false);
        HowToPlayMenu.SetActive(false);
    }

    /// <summary>
    /// Load the main menu
    /// </summary>
    public void ShowMainMenu() {
        HideAllMenus();
        MainMenu.SetActive(true);
    }


    /// <summary>
    /// Game Options
    /// </summary>
    public void GameOptions() {
        HideAllMenus();
        OptionsMenu.SetActive(true);
    }

    /// <summary>
    /// New Local Game Menu
    /// </summary>
    public void NewLocalGame() {
        HideAllMenus();
        LocalPlayMenu.SetActive(true);
    }

    /// <summary>
    /// New Online Game Menu
    /// </summary>
    public void NewOnlineGame() {
        HideAllMenus();
        OnlinePlayMenu.SetActive(true);
    }

    /// <summary>
    /// Quit Game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
