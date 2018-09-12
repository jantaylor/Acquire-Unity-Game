using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int _numOfPlayers = Constants.DefaultNumberOfPlayers;
    private Queue<Player> _turnOrder = new Queue<Player>();

    public int TurnNumber = 0;
    public static GameManager Instance = null;
    public PlayerController PlayerController;
    public MoneyController MoneyController;
    public CorporationController CorporationController;
    public TileController TileController;
    public BoardController BoardController;
    public HudController HudController;

    private void Awake() {
        // Singleton setup for GameManager
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        PlayerController = GetComponent<PlayerController>();
        MoneyController = GetComponent<MoneyController>();
        CorporationController = GetComponent<CorporationController>();
        TileController = GetComponent<TileController>();
        BoardController = GetComponent<BoardController>();
        HudController = GetComponent<HudController>();
    }

    private void Start() {
        NewGame();
    }

    // Setup a new game
    private void NewGame() {
        AddPlayers();
        StartingTiles();
        StartingHands();
    }

    private void StartingTiles() {
        // Draw starting tile per player
        int[] drawnTileIds = new int[_numOfPlayers];
        foreach (Player player in PlayerController.Players()) {
            DrawTile(player);
            Tile drawnTile = player.Tiles[0];
            PlayTile(player, drawnTile);
            drawnTileIds[player.Id] = drawnTile.Id;
        }
        EstablishTurnOrder(drawnTileIds);
        PrintTurnOrder();
    }

    private void StartingHands() {
        foreach (Player player in PlayerController.Players()) {
            // Draw rest of tiles for starting hand
            DrawTile(player, 6);
            PlayerController.GetPlayerTiles(player);
            UpdateHud(player);
        }
    }

    private void EstablishTurnOrder(int[] drawnTiles) {
        // 0 - 23, 1 - 13, 2 - 24
        int min = 0;
        Player[] playerArray = new Player[_numOfPlayers];
        for (int i = 0; i < _numOfPlayers; ++i) {
            int curr = drawnTiles[i];
            if (curr >= min) {
                _turnOrder.Enqueue(PlayerController.Player(i));
                min = curr;
            } else {
                for (int j = 0; j < i; ++j) {
                    playerArray[j] = _turnOrder.Dequeue();
                }
                for (int k = 0; k < i; ++k) {
                    if (curr > drawnTiles[playerArray[k].Id]) {
                        _turnOrder.Enqueue(playerArray[k]);
                    } else {
                        _turnOrder.Enqueue(PlayerController.Player(i));
                        curr = 0;
                    }
                }
            }
        }
    }

    private void PrintTurnOrder() {
        for (int i = 0; i < _turnOrder.Count; ++i) {
            Player player = _turnOrder.Dequeue();
            Debug.Log("#" + (i + 1) + " - " + player.Name);
            _turnOrder.Enqueue(player);
        }
    }

    private void UpdateHud(Player player) {
        HudController.SetPlayerName(player.Name);
        HudController.SetWalletAmount(MoneyController.PlayerAmount(player));
        HudController.UpdatePlayerStock(player.Stocks);
        // Add Tiles to Side for Player
        foreach (Tile tile in player.Tiles)
            HudController.SetPlayerTiles(tile);
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
        PlayerController.CreatePlayers(_numOfPlayers);
        MoneyController.CreateWallets(PlayerController.Players());
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
        Tile drawnTile = TileController.DrawTile();
        PlayerController.GivePlayerTile(player, drawnTile);
        TileController.PrintTile(drawnTile);
    }

    public void DrawTile(Player player, int amountToDraw) {
        for (int i = 0; i < amountToDraw; ++i) {
            PlayerController.GivePlayerTile(player, TileController.DrawTile());
        }
    }

    #endregion

    #region Game Manager Public

    public void PlayTile(Player player, Tile tile) {
        PlayerController.RemovePlayerTile(player, tile);
        GameObject newTile = TileController.CreateTileObject(tile, tile.Position);
        BoardController.PlaceTileOnBoard(newTile);
    }

    public void Endturn() {
        Debug.Log("Ending turn for Player");
        ++TurnNumber;
    }

    #endregion
}
