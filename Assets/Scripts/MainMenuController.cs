using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    private bool _singlePlayer = false;
    private int _numberOfAi = Constants.DefaultNumberOfAi;
    private int _numberOfPlayers = Constants.DefaultNumberOfPlayers;
    private int _aiDifficulty = Constants.DefaultAiDifficulty; //1 - easy, 2 - medium, 3 - hard

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
        _singlePlayer = false;
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
        _singlePlayer = false;
    }

    public void ShowSinglePlayerGame() {
        HideAllMenus();
        SinglePlayerMenu.SetActive(true);
        _singlePlayer = true;
    }

    public void StartSinglePlayerGame() {
        // TODO: Setup
        _numberOfPlayers = 1;
        SceneManager.LoadScene("Game");
    }

    public void StartHotSeatGame() {
        // TODO: Setup
        SceneManager.LoadScene("Game");
    }

    public void ChangeNumberOfAi() {
        if (_numberOfAi < Constants.MaxNumberOfAi) {
            ++_numberOfAi;
            if (_numberOfPlayers + _numberOfAi < Constants.MaxNumberOfPlayers) {
                --_numberOfPlayers;
            }
        } else {
            if (_singlePlayer)
                _numberOfAi = 1;
            else
                _numberOfAi = 0;
        }
        GameObject.Find("NumberOfAiButton").GetComponentInChildren<Text>().text = "AI Players: " + _numberOfAi.ToString();
        GameObject.Find("NumberOfPlayersButton").GetComponentInChildren<Text>().text = "Players: " + _numberOfPlayers.ToString();
    }

    public void ChangeAiDifficulty() {
        if (_aiDifficulty < 3) {
            ++_aiDifficulty;
        } else {
            _aiDifficulty = 1;
        }
        string aiDifficultyText = "Easy";
        switch(_aiDifficulty) {
            case 1:
                aiDifficultyText = "Easy";
                break;
            case 2:
                aiDifficultyText = "Medium";
                break;
            case 3:
                aiDifficultyText = "Hard";
                break;
            default:
                aiDifficultyText = "EXTREME";
                break;
        }
        GameObject.Find("AiDifficultyButton").GetComponentInChildren<Text>().text = "AI Difficulty: " + aiDifficultyText;
    }

    public void ChangeNumberOfPlayers() {
        if (_numberOfPlayers < Constants.MaxNumberOfPlayers) {
            ++_numberOfPlayers;
            if (_numberOfPlayers + _numberOfAi < Constants.MaxNumberOfPlayers) {
                --_numberOfAi;
            }
        } else {
            _numberOfPlayers = 2;
        }
        GameObject.Find("NumberOfPlayersButton").GetComponentInChildren<Text>().text = "Players: " + _numberOfPlayers.ToString();
        GameObject.Find("NumberOfAiButton").GetComponentInChildren<Text>().text = "AI Players: " + _numberOfAi.ToString();
    }
}
