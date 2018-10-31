using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    /// <summary>
    /// Create an instance of the game class in memory called State
    /// </summary>
    public static Game State;

    public int NumberOfPlayers { get; set; }

    public int NumberOfAi { get; set; }

    public int AiDifficulty { get; set; }

    public GameLog GameLog;

    /// <summary>
    /// On starting the game, singleton state created
    /// </summary>
    void Awake() {
        if (State == null) {
            DontDestroyOnLoad(gameObject);
            State = this;
        } else if (State != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        SetDefaults();
    }

    public void Set(int numberOfPlayers, int numberOfAi, int aiDifficulty) {
        NumberOfPlayers = numberOfPlayers;
        NumberOfAi = numberOfPlayers;
        AiDifficulty = aiDifficulty;
    }

    /// <summary>
    /// Set Defaults for each new game, will be overwritten by data load
    /// </summary>
    public void SetDefaults() {
        NumberOfPlayers = Constants.DefaultNumberOfPlayers;
        NumberOfAi = Constants.DefaultNumberOfAi;
        AiDifficulty = Constants.DefaultAiDifficulty;
    }

    /// <summary>
    /// Log to the game logger using GameLog or Game.State.Log
    /// </summary>
    /// <param name="eventString"></param>
    public void Log(string eventString) {
        GameLog.Log(eventString);
    }

    /// <summary>
    /// Load the main menu from the game
    /// </summary>
    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}