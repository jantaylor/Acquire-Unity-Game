using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;
    private int _numOfPlayers = Constants.DefaultNumberOfPlayers;
    // TODO: Maybe do Queue<Player> and use this for hotseat to pass information on HUD
    private Queue<Player> _turnOrder = new Queue<Player>();

    private PlayerController _playerController;
    private MoneyController _moneyController;
    private CorporationController _corporationController;
    private TileController _tileController;
    private BoardController _boardController;
    private HudController _hudController;

    private void Awake() {
        // Singleton setup for GameManager
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        _playerController = GetComponent<PlayerController>();
        _moneyController = GetComponent<MoneyController>();
        _corporationController = GetComponent<CorporationController>();
        _tileController = GetComponent<TileController>();
        _boardController = GetComponent<BoardController>();
        _hudController = GetComponent<HudController>();
    }

    private void Start() {
        NewGame();
    }

    // Setup a new game
    private void NewGame() {
        _boardController.SetupBoard();
        AddPlayers();
        ShuffleTurnOrder();

        Player player1 = _playerController.Player(0);
        UpdateHud(player1);
        DrawTile(player1);
        _playerController.GetPlayerTiles(player1);
        DrawTile(player1, 6);
        _playerController.GetPlayerTiles(player1);
    }

    private void UpdateHud(Player player) {
        _hudController.SetPlayerName(player.Name);
        _hudController.SetWalletAmount(_moneyController.PlayerAmount(player));
        _hudController.UpdatePlayerStock(player.Stocks);
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void RestartGame() {
        // TOOD: Garbage Collection here
        NewGame();
    }

    //private void Update() {
    //    if (IsGameOver())
    //        GameOver();
    //}

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
    private Player PlayerTurn() {
        return _turnOrder.Peek();
    }

    /// <summary>
    /// TODO
    /// </summary>
    private void AddPlayers() {
        _playerController.CreatePlayers(_numOfPlayers);
        _moneyController.CreateWallets(_playerController.Players());
    }

    /// <summary>
    /// Returns the next player's id
    /// </summary>
    /// <returns>Next player id</returns>
    private Player NextPlayer() {
        Player nextPlayer = _turnOrder.Dequeue();
        _turnOrder.Enqueue(nextPlayer);
        return nextPlayer;
    }


    /// <summary>
    /// Shifts the players turn order around and sets it up
    /// </summary>
    private void ShuffleTurnOrder() {
        Player[] turnOrderArray = _turnOrder.ToArray();
        _turnOrder.Clear();
        CommonFunctions.Shuffle<Player>(new System.Random(), turnOrderArray);
        foreach (Player player in turnOrderArray)
            _turnOrder.Enqueue(player);
    }

    #region Tile Controller Public

    public void DrawTile(Player player) {
        Tile drawnTile = _tileController.DrawTile();
        _playerController.GivePlayerTile(player, drawnTile);
    }

    public void DrawTile(Player player, int amountToDraw) {
        for (int i = 0; i < amountToDraw; ++i) {
            _playerController.GivePlayerTile(player, _tileController.DrawTile());
        }
    }

    #endregion

    #region Game Manager Public

    public void Endturn() {
        Debug.Log("Ending turn for Player");
        // TODO
    }

    #endregion
}
