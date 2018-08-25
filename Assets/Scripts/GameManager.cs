using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;
    private BoardController _boardController;
    private int _numOfPlayers = 3;
    private Player[] _players;
    private Queue<int> _turnOrder;


    private void Awake() {
        // Singleton setup for GameManager
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        // Don't destroy this on scene load
        DontDestroyOnLoad(gameObject);

        _boardController = GetComponent<BoardController>();

        NewGame();
    }

    private void Update() {
        if (IsGameOver())
            GameOver();
    }

    // Setup a new game
    private void NewGame() {
        _boardController.SetupBoard();
        AddPlayers();
        ShuffleTurnOrder();
    }

    /// <summary>
    /// TODO
    /// </summary>
    private void RestartGame() {

    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    private bool IsGameOver() {
        return false;
    }

    /// <summary>
    /// TODO
    /// </summary>
    private void GameOver() {

    }

    /// <summary>
    /// Returns next players turn for Players[]
    /// </summary>
    /// <returns>int</returns>
    private int PlayerTurn() {
        return _turnOrder.Peek();
    }

    /// <summary>
    /// TODO
    /// </summary>
    private void AddPlayers() {
        
    }

    /// <summary>
    /// Returns the next player's id
    /// </summary>
    /// <returns>Next player id</returns>
    private int NextPlayer() {
        int nextPlayer = _turnOrder.Dequeue();
        _turnOrder.Enqueue(nextPlayer);
        return nextPlayer;
    }


    /// <summary>
    /// Shifts the players turn order around and sets it up
    /// </summary>
    private void ShuffleTurnOrder() {
        int[] turnOrderArray = _turnOrder.ToArray();
        _turnOrder.Clear();
        CommonFunctions.Shuffle<int>(new System.Random(), turnOrderArray);
        foreach (int turn in turnOrderArray)
            _turnOrder.Enqueue(turn);
    }
}
