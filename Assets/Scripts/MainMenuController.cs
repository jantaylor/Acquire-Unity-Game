using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject LocalPlayMenu;
    public GameObject OnlinePlayMenu;
    public GameObject HowToPlayMenu;
    public GameObject SinglePlayerMenu;
    public GameObject HotSeatMenu;

    private void Awake() {
        if (!MainMenu) MainMenu = GameObject.Find("MainMenu");
        if (!OptionsMenu) OptionsMenu = GameObject.Find("OptionsMenu");
        if (!LocalPlayMenu) LocalPlayMenu = GameObject.Find("LocalPlayMenu");
        if (!OnlinePlayMenu) OnlinePlayMenu = GameObject.Find("OnlinePlayMenu");
        if (!HowToPlayMenu) HowToPlayMenu = GameObject.Find("HowToPlayMenu");

        if (!SinglePlayerMenu) SinglePlayerMenu = GameObject.Find("SinglePlayerMenu");
        if (!HotSeatMenu) HotSeatMenu = GameObject.Find("HotSeatMenu");
    }

    private void Start() {
        ShowMainMenu();
    }

    private void HideAllMenus() {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        LocalPlayMenu.SetActive(false);
        OnlinePlayMenu.SetActive(false);
        HowToPlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(false);
        HotSeatMenu.SetActive(false);
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
    public void ShowGameOptions() {
        HideAllMenus();
        OptionsMenu.SetActive(true);
    }

    /// <summary>
    /// New Local Game Menu
    /// </summary>
    public void ShowLocalMenu() {
        HideAllMenus();
        LocalPlayMenu.SetActive(true);
    }

    /// <summary>
    /// New Online Game Menu
    /// </summary>
    public void ShowOnlineMenu() {
        HideAllMenus();
        OnlinePlayMenu.SetActive(true);
    }

    public void ShowHowToPlayMenu() {
        HideAllMenus();
        HowToPlayMenu.SetActive(true);
    }

    /// <summary>
    /// Quit Game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// Show Hot Seat Game Menu
    /// </summary>
    public void ShowHotSeatGame() {
        HideAllMenus();
        HotSeatMenu.SetActive(true);
    }

    public void ShowSinglePlayerGame() {
        HideAllMenus();
        SinglePlayerMenu.SetActive(true);
    }

    public void StartSinglePlayerGame() {
        // TODO: Setup
        SceneManager.LoadScene("Game");
    }

    public void StartHotSeatGame() {
        // TODO: Setup
        SceneManager.LoadScene("Game");
    }
}
