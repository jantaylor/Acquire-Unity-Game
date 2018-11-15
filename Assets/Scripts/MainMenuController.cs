using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public bool _singlePlayer = false;
    public int _numberOfAi = Constants.DefaultNumberOfAi;
    public int _numberOfPlayers = Constants.DefaultNumberOfPlayers;
    public int _aiDifficulty = Constants.DefaultAiDifficulty; //1 - easy, 2 - medium, 3 - hard

    public string Port = Constants.DefaultPort.ToString();
    public string Name = Constants.DefaultPlayerName;
    public string Room = Constants.DefaultRoom;

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject LocalPlayMenu;
    public GameObject OnlinePlayMenu;
    public GameObject JoinGameMenu;
    public GameObject HostGameMenu;
    public GameObject HowToPlayMenu;
    public GameObject SinglePlayerMenu;
    public GameObject HotSeatMenu;

    public NetworkManager NetworkManager;

    private void Awake() {
        if (!MainMenu) MainMenu = GameObject.Find("MainMenu");
        if (!OptionsMenu) OptionsMenu = GameObject.Find("OptionsMenu");
        if (!HowToPlayMenu) HowToPlayMenu = GameObject.Find("HowToPlayMenu");

        if (!OnlinePlayMenu) OnlinePlayMenu = GameObject.Find("OnlinePlayMenu");
        if (!JoinGameMenu) JoinGameMenu = GameObject.Find("JoinGameMenu");
        if (!HostGameMenu) HostGameMenu = GameObject.Find("HostGameMenu");

        if (!LocalPlayMenu) LocalPlayMenu = GameObject.Find("LocalPlayMenu");
        if (!SinglePlayerMenu) SinglePlayerMenu = GameObject.Find("SinglePlayerMenu");
        if (!HotSeatMenu) HotSeatMenu = GameObject.Find("HotSeatMenu");

        if (!NetworkManager) NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Start() {
        ShowMainMenu();
    }

    private void HideAllMenus() {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        LocalPlayMenu.SetActive(false);
        OnlinePlayMenu.SetActive(false);
        JoinGameMenu.SetActive(false);
        HostGameMenu.SetActive(false);
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
        _singlePlayer = Constants.DefaultSinglePlayer;
        _numberOfAi = Constants.DefaultNumberOfAi;
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
        //if (Network.isClient && Network.isServer) {
        //    GameObject.Find("JoinGameButton").GetComponent<Button>().interactable = false;
        //    GameObject.Find("HostGameButton").GetComponent<Button>().interactable = false;
        //} else {
        //    GameObject.Find("JoinGameButton").GetComponent<Button>().interactable = true;
        //    GameObject.Find("HostGameButton").GetComponent<Button>().interactable = true;
        //}
    }

    /// <summary>
    /// Join Online Game Menu
    /// </summary>
    public void ShowJoinMenu() {
        HideAllMenus();
        JoinGameMenu.SetActive(true);
    }

    /// <summary>
    /// Host Online Game Menu
    /// </summary>
    public void ShowHostMenu() {
        HideAllMenus();
        HostGameMenu.SetActive(true);
    }

    /// <summary>
    /// Create and join the online game using the matchInfo, config, and port
    /// </summary>
    public void HostOnlineGame() {
        // TODO
    }

    /// <summary>
    /// Join the online game using the matchInfo, config, and port
    /// </summary>
    public void JoinOnlineGame() {
        // TODO
    }

    public void UpdatePort(string newPort) {
        Port = newPort;
    }

    public void UpdateName(string newName) {
        Name = newName;
    }

    public void UpdateRoom(string newRoom) {
        Room = newRoom;
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
        _numberOfAi = 0;
    }

    public void ShowSinglePlayerGame() {
        HideAllMenus();
        SinglePlayerMenu.SetActive(true);
        _singlePlayer = true;
    }

    public void StartSinglePlayerGame() {
        _numberOfPlayers = 1;
        Game.State.Set(_numberOfPlayers, _numberOfAi, _aiDifficulty);
        SceneManager.LoadScene("Game");
    }

    public void StartHotSeatGame() {
        Game.State.Set(_numberOfPlayers, _numberOfAi, _aiDifficulty);
        SceneManager.LoadScene("Game");
    }

    public void ChangeNumberOfAi() {
        if (_numberOfAi < Constants.MaxNumberOfAi && (!_singlePlayer && _numberOfAi < Constants.MaxNumberOfAi - 1)) {
            ++_numberOfAi;
            if (_numberOfPlayers + _numberOfAi > Constants.MaxNumberOfPlayers) {
                if (_numberOfPlayers > 2) --_numberOfPlayers;
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
            if (_numberOfPlayers + _numberOfAi >= Constants.MaxNumberOfPlayers) {
                if (_numberOfAi > 0) --_numberOfAi;
            }
        } else {
            _numberOfPlayers = 2;
        }
        GameObject.Find("NumberOfPlayersButton").GetComponentInChildren<Text>().text = "Players: " + _numberOfPlayers.ToString();
        GameObject.Find("NumberOfAiButton").GetComponentInChildren<Text>().text = "AI Players: " + _numberOfAi.ToString();
    }
}
