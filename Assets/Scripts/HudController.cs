using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    private List<PlayerHud> _playerHuds = new List<PlayerHud>();
    private Vector3[] _hudPositions = new Vector3[2];
    private Vector2[] _pivotAnchorPositions = new Vector2[2];
    private Button[] stockButtons = new Button[7];
    public GameObject PlayerHudPrefab;
    public GameObject HudCanvas;
    public GameObject BuyStockCanvas;

    void Awake() {
        _hudPositions[0] = new Vector3(10f, -10f);
        _hudPositions[1] = new Vector3(-10f, -10f);
        _pivotAnchorPositions[0] = new Vector2(0f, 1f);
        _pivotAnchorPositions[1] = new Vector2(1f, 1f);
        // TODO: Add additional hud positions

        stockButtons = BuyStockCanvas.GetComponentsInChildren<Button>();
    }

    public void CreatePlayerHuds(List<Player> players) {
        int playerNum = 0;
        foreach (Player player in players) {
            GameObject newPlayerHudObj = Instantiate(PlayerHudPrefab);

            RectTransform newPlayerRect = newPlayerHudObj.GetComponent<RectTransform>();
            newPlayerHudObj.transform.SetParent(HudCanvas.transform);
            newPlayerRect.pivot = _pivotAnchorPositions[playerNum];
            newPlayerRect.anchorMin = _pivotAnchorPositions[playerNum];
            newPlayerRect.anchorMax = _pivotAnchorPositions[playerNum];
            newPlayerRect.anchoredPosition = _hudPositions[playerNum];

            PlayerHud newPlayerHud = newPlayerHudObj.GetComponent<PlayerHud>();
            newPlayerHud.AssignPlayerToHud(player);
            _playerHuds.Add(newPlayerHud);
            ++playerNum;
        }
    }

    public void UpdatePlayerHud(Player player) {
        BuyStockCanvas.SetActive(false);
        _playerHuds.Find(p => p.Player == player).UpdatePlayerHud();
    }

    public void UpdatePlayersHud(List<Player> players) {
        BuyStockCanvas.SetActive(false);
        foreach (Player player in players)
            _playerHuds.Find(p => p.Player == player).UpdatePlayerHud();
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
        BuyStockCanvas.SetActive(true);
    }

    public void HideBuyStockHud() {
        BuyStockCanvas.SetActive(false);
    }

    public void OptionsToBuy() {
        foreach (Corporation corp in GameManager.Instance.CorporationController.Corporations) {
            if (corp.TileSize > 0) {
                Debug.Log("Available: " + corp.Name + ", Tile Size: " + corp.TileSize + ", Available Stocks: " + corp.Stocks.Count);
                stockButtons[corp.Id].gameObject.SetActive(true);
            } else {
                Debug.Log("Not Available: " + corp.Name + " Available Stocks: " + corp.Stocks.Count);
                stockButtons[corp.Id].gameObject.SetActive(false);
            }
        }
    }

    public void BuyStock(int id) {
        GameManager.Instance.CorporationController.BuyStock(GameManager.Instance.ActivePlayer, id, 1);
    }
}
