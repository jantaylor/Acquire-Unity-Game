using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Queue<Player> _turnOrder = new Queue<Player>();
    public Player ActivePlayer;
    public string ActivePlayerName;
    public bool TilePlaced = false;
    public int StocksPurchased = 0;
    public int NumberOfPlayers = Constants.DefaultNumberOfPlayers;
    public int NumberOfAI = Constants.DefaultNumberOfAi;
    public int AiDifficulty = Constants.DefaultAiDifficulty;
    public int TurnNumber = 0;
    public bool AllCorporationsOnBoard = false;

    public static GameManager Instance = null;
    public PlayerController PlayerController;
    public MoneyController MoneyController;
    public CorporationController CorporationController;
    public TileController TileController;
    public BoardController BoardController;
    public HudController HudController;
    public MainMenuController MainMenuController;

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
        MainMenuController = GetComponent<MainMenuController>();
    }

    private void Start() {
        LoadState();
        NewGame();
    }

    private void Update() {
        CheckForAllCorpsOnBoard();
    }

    /// <summary>
    /// Load the game setup from state
    /// </summary>
    private void LoadState() {
        Game.State.GameLog = GameObject.Find("UI/Canvas/GameLog HUD").GetComponent<GameLog>();
        NumberOfPlayers = Game.State.NumberOfPlayers;
        NumberOfAI = Game.State.NumberOfAi;
        AiDifficulty = Game.State.AiDifficulty;
    }

    // Setup a new game
    private void NewGame() {
        AddPlayers();
        StartingTiles();
        StartingHands();
        NextPlayer();
        HudController.ShowNotificationHud();
        Game.State.Log(ActivePlayer.Name + " goes first!");
    }

    private void StartingTiles() {
        // Draw starting tile per player
        foreach (Player player in PlayerController.Players()) {
            DrawTile(player);
        }

        EstablishTurnOrder();
        PrintTurnOrder(); // TODO: For Debugging

        foreach (Player player in PlayerController.Players()) {
            Tile drawnTile = player.Tiles[0];
            PlaceStartingTile(player, drawnTile);
        }
    }

    private void EstablishTurnOrder() {
        List<Player> sortedList = new List<Player>();
        sortedList = PlayerController.Players().OrderBy(p => p.Tiles[0].Id).ToList();
        foreach (Player player in sortedList)
            _turnOrder.Enqueue(player);
        sortedList = null; // Remove sortedList
    }

    private void PrintTurnOrder() {
        for (int i = 0; i < _turnOrder.Count; ++i) {
            Player player = _turnOrder.Dequeue();
            Game.State.Log(player.Name + " drew tile "
                + player.Tiles[0].Number + player.Tiles[0].Letter
                + " (" + player.Tiles[0].Id + ").");
            _turnOrder.Enqueue(player);
        }
    }

    private void StartingHands() {
        foreach (Player player in PlayerController.Players()) {
            // Draw rest of tiles for starting hand
            DrawTile(player, 6);
            PlayerController.GetPlayerTiles(player);
            HudController.UpdatePlayerHud(player);
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void RestartGame() {
        // TOOD: Garbage Collection here
        NewGame();
    }

    private void CheckForAllCorpsOnBoard() {
        foreach (Corporation corp in CorporationController.Corporations)
            if (corp.TileSize > 0)
                AllCorporationsOnBoard = true;
            else
                AllCorporationsOnBoard = false;
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
    /// TODO
    /// </summary>
    private void AddPlayers() {
        PlayerController.CreatePlayers(NumberOfPlayers);
        MoneyController.CreateWallets(PlayerController.Players());
        HudController.CreatePlayerHuds(PlayerController.Players());
    }

    private void NextPlayer() {
        ActivePlayer = _turnOrder.Dequeue();
        _turnOrder.Enqueue(ActivePlayer);
        ActivePlayerName = ActivePlayer.Name;
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
        HudController.AddPlayerTile(player, drawnTile);
    }

    public void DrawTile(Player player, int amountToDraw) {
        for (int i = 0; i < amountToDraw; ++i) {
            Tile drawnTile = TileController.DrawTile();
            PlayerController.GivePlayerTile(player, drawnTile);
            HudController.AddPlayerTile(player, drawnTile);
        }
    }

    #endregion

    #region Game Manager Public

    public void PlaceStartingTile(Player player, Tile tile) {
        PlayerController.RemovePlayerTile(player, tile);
        GameObject newTile = TileController.CreateTileObject(tile, tile.Position);
        BoardController.PlaceTileOnBoard(newTile, true);
        HudController.RemovePlayerTile(player, tile);
        ++GameManager.Instance.TurnNumber; // We still need to increase the turn order for the history array
    }

    public void Endturn() {
        if (TilePlaced) {
            Game.State.Log("Ending " + ActivePlayer.Name + "'s turn.");
            HudController.HidePlayerHud(GameManager.Instance.ActivePlayer);
            HudController.HideGameLog();
            DrawTile(ActivePlayer);
            TilePlaced = false;
            StocksPurchased = 0;
            NextPlayer();
            HudController.HideBuyStockHud();
            HudController.ShowNotificationHud();
            Game.State.Log("It's your turn " + ActivePlayer.Name + ".");
            ++TurnNumber;

            // Debugging

            //foreach (Corporation corp in CorporationController.Corporations) {
            //    if (corp.TileSize > 0) {
            //        Debug.Log(corp.Name + " has tiles: ");
            //        foreach (GameObject tile in corp.Tiles)
            //            Debug.Log(tile.GetComponent<TileObject>().Tile.Number + tile.GetComponent<TileObject>().Tile.Letter + " ");
            //    } else {
            //        Debug.Log(corp.Name + " has no tiles.");
            //    }
            //}
                
                    
        } else {
            // TODO: Maybe make this a notification that shows up and disappears
            Debug.Log("Can't end your turn before placing a tile!");
        }
    }

    #endregion

    #region Stock Related Public

    public void BuyStock() {
        HudController.ShowBuyStockHUD();
    }

    #endregion
}