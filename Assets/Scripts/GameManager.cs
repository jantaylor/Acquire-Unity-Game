using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;
    private int _numOfPlayers = Constants.DefaultNumberOfPlayers;
    // TODO: Maybe do Queue<Player> and use this for hotseat to pass information on HUD
    private Queue<Player> _turnOrder = new Queue<Player>();

    public PlayerController playerController;
    public MoneyController moneyController;
    public CorporationController corporationController;
    public TileController tileController;
    public BoardController boardController;
    public HudController hudController;

    private void Awake() {
        // Singleton setup for GameManager
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        playerController = GetComponent<PlayerController>();
        moneyController = GetComponent<MoneyController>();
        corporationController = GetComponent<CorporationController>();
        tileController = GetComponent<TileController>();
        boardController = GetComponent<BoardController>();
        hudController = GetComponent<HudController>();
    }

    private void Start() {
        NewGame();
    }

    // Setup a new game
    private void NewGame() {
        boardController.SetupBoard();
        AddPlayers();
        ShuffleTurnOrder();

        Player player1 = playerController.Player(0);
        DrawTile(player1);
        playerController.GetPlayerTiles(player1);
        //DrawTile(player1, 6);
        //playerController.GetPlayerTiles(player1);
        UpdateHud(player1);
    }

    private void UpdateHud(Player player) {
        hudController.SetPlayerName(player.Name);
        hudController.SetWalletAmount(moneyController.PlayerAmount(player));
        hudController.UpdatePlayerStock(player.Stocks);
        // Add Tiles to Side for Player
        foreach (Tile tile in player.Tiles)
            tileController.CreateTileObject(tile, new Vector3(0, 0, 0));
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
        playerController.CreatePlayers(_numOfPlayers);
        moneyController.CreateWallets(playerController.Players());
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
        Tile drawnTile = tileController.DrawTile();
        playerController.GivePlayerTile(player, drawnTile);
    }

    public void DrawTile(Player player, int amountToDraw) {
        for (int i = 0; i < amountToDraw; ++i) {
            playerController.GivePlayerTile(player, tileController.DrawTile());
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
