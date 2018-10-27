using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    private List<PlayerHud> _playerHuds = new List<PlayerHud>();
    private Vector3 _hudPosition = new Vector3(10f, -10f);
    private Vector2 _pivotAnchorPosition = new Vector2(0f, 1f);
    private Button[] stockButtons = new Button[7];
    public GameObject PlayerHudPrefab;
    public GameObject HudCanvas;
    public GameObject NotificationHud;
    public GameObject GameLogHud;
    public GameObject BuyStockHud;

    public GameLog GameLog;

    void Awake() {
        GameLog = GameLogHud.GetComponentInChildren<GameLog>();
        stockButtons = BuyStockHud.GetComponentsInChildren<Button>();
    }

    public void CreatePlayerHuds(List<Player> players) {
        int playerNum = 0;
        foreach (Player player in players) {
            GameObject newPlayerHudObj = Instantiate(PlayerHudPrefab);

            RectTransform newPlayerRect = newPlayerHudObj.GetComponent<RectTransform>();
            newPlayerHudObj.transform.SetParent(HudCanvas.transform);
            newPlayerRect.pivot = _pivotAnchorPosition;
            newPlayerRect.anchorMin = _pivotAnchorPosition;
            newPlayerRect.anchorMax = _pivotAnchorPosition;
            newPlayerRect.anchoredPosition = _hudPosition;

            PlayerHud newPlayerHud = newPlayerHudObj.GetComponent<PlayerHud>();
            newPlayerHud.AssignPlayerToHud(player);
            _playerHuds.Add(newPlayerHud);
            newPlayerHud.playerHud = newPlayerHudObj;
            ++playerNum;

            // Hide the new HUDs
            newPlayerHudObj.SetActive(false);
        }
    }

    //public void CreateGameLogHud() {
    //    RectTransform gameLogRect = GameLogCanvas.GetComponent<RectTransform>();
    //    gameLogRect.pivot = _pivotAnchorPositions[1];
    //    gameLogRect.anchorMin = _pivotAnchorPositions[1];
    //    gameLogRect.anchorMax = _pivotAnchorPositions[1];
    //    gameLogRect.anchoredPosition = _hudPositions[1];
    //}

    public void UpdatePlayerHud(Player player) {
        // Hide the BuyStocksCanvas if they purchased 3
        if (GameManager.Instance.StocksPurchased >= 3)
            BuyStockHud.SetActive(false);
        _playerHuds.Find(p => p.Player == player).UpdatePlayerHud();
    }

    public void UpdatePlayersHud(List<Player> players) {
        BuyStockHud.SetActive(false);
        foreach (Player player in players)
            _playerHuds.Find(p => p.Player == player).UpdatePlayerHud();
    }

    /// <summary>
    /// hide player hud
    /// </summary>
    public void HidePlayerHud(Player player) {
        _playerHuds.Find(p => p.Player == player).playerHud.SetActive(false);
    }

    /// <summary>
    /// show player hud
    /// </summary>
    public void ShowPlayerHud(Player player) {
        _playerHuds.Find(p => p.Player == player).playerHud.SetActive(true);
    }

    /// <summary>
    /// Show only the active player's hud, usually after they click on the notification canvas
    /// </summary>
    public void ShowOnlyActivePlayerHud() {
        _playerHuds.ForEach(p => p.playerHud.SetActive(false));
        _playerHuds.Find(p => p.Player == GameManager.Instance.ActivePlayer).playerHud.SetActive(true);
        ShowGameLog();
    }

    /// <summary>
    /// hide notification hud
    /// </summary>
    public void HideNotificationHud() {
        NotificationHud.SetActive(false);
    }

    /// <summary>
    /// show notification hud
    /// </summary> 
    public void ShowNotificationHud () {
        NotificationHud.SetActive(true);
        NotificationHud.transform.Find("TurnContinueBtn/Title").GetComponentInChildren<Text>().text = GameManager.Instance.ActivePlayer.Name + ", it is now your turn.";
    }

    /// <summary>
    /// hide game log
    /// </summary>
    public void HideGameLog() {
        GameLogHud.SetActive(false);
    }

    /// <summary>
    /// show the game log
    /// </summary>
    public void ShowGameLog() {
        GameLogHud.SetActive(true);
    }

    public void SetPlayerName(Player player, string newName) {
        _playerHuds.Find(p => p.Player == player).SetPlayerName(newName);
    }

    public void SetWalletAmount(Player player, int newAmount) {
        _playerHuds.Find(p => p.Player == player).SetWalletAmount(newAmount);
    }

    public void UpdatePlayerStocks(Player player, List<Stock> stocks) {
        _playerHuds.Find(p => p.Player == player).UpdatePlayerStocks(stocks);
    }

    public void AddPlayerTile(Player player, Tile tile) {
        _playerHuds.Find(p => p.Player == player).AddPlayerTile(player, tile);
    }

    public void SetPlayerTiles(Player player, List<Tile> tiles) {
        _playerHuds.Find(p => p.Player == player).SetPlayerTiles(player, tiles);
    }

    public void RemovePlayerTile(Player player, Tile tile) {
        _playerHuds.Find(p => p.Player == player).RemovePlayerTile(player, tile);
    }

    public void ShowBuyStockHUD() {
        OptionsToBuy();
        BuyStockHud.SetActive(true);
    }

    public void HideBuyStockHud() {
        BuyStockHud.SetActive(false);
    }

    public void OptionsToBuy() {
        foreach (Corporation corp in GameManager.Instance.CorporationController.Corporations) {
            if (corp.TileSize >= 1 && corp.Stocks.Count >= 1) {
                Debug.Log("Available: " + corp.Name + ", Tile Size: " + corp.TileSize + ", Available Stocks: " + corp.Stocks.Count);
                stockButtons[corp.Id].GetComponentInChildren<Text>().text = corp.Name + "\n$" + corp.StockValue.ToString();
                stockButtons[corp.Id].gameObject.SetActive(true);
            } else {
                Debug.Log("Not Available: " + corp.Name + " Available Stocks: " + corp.Stocks.Count);
                stockButtons[corp.Id].gameObject.SetActive(false);
            }
        }
    }

    public void BuyStock(int id) {
        Debug.Log("Trying to buy stock for " + GameManager.Instance.CorporationController.Corporation(id).Name);
        GameManager.Instance.CorporationController.BuyStock(GameManager.Instance.ActivePlayer, id, 1);
        UpdatePlayerHud(GameManager.Instance.ActivePlayer);
    }

    /// <summary>
    /// Upon clicking the notification hud, hide the notification hud and show only the active player's hud
    /// </summary>
    public void NextPlayerContinueClick() {
        HideNotificationHud();
        ShowOnlyActivePlayerHud();
        //ShowGameLog();
    }
}
