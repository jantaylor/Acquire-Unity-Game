using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour {

    public Player Player;

    public Text PlayerNameText;
    public Text WalletAmountText;
    public Text NestorStockText;
    public Text SparkStockText;
    public Text EtchStockText;
    public Text RoveStockText;
    public Text FleetStockText;
    public Text EchoStockText;
    public Text BoltStockText;
    public Button BuyStockButton;
    public Button EndTurnButton;
    public Transform TileGrid;

    public GameObject tilePrefab;

    private void Start() {
        // TODO: Buy Stock
        //BuyStockButton.onClick.AddListener();
        EndTurnButton.onClick.AddListener(GameManager.Instance.Endturn);
    }

    private void Update() {
        if (GameManager.Instance.PlayerController.ActivePlayer != Player) {
            BuyStockButton.interactable = false;
            EndTurnButton.interactable = false;
        } else {
            // TODO: change 0 to 3
            if (GameManager.Instance.StocksPurchased < 0)
                BuyStockButton.interactable = true;
            EndTurnButton.interactable = true;
        }
}

    public void AssignPlayerToHud(Player player) {
        Player = player;
        UpdatePlayerHud();
        SetPlayerTiles(Player, Player.Tiles);
    }

    public void UpdatePlayerHud() {
        SetPlayerName(Player.Name);
        SetWalletAmount(GameManager.Instance.MoneyController.PlayerAmount(Player));
        UpdatePlayerStocks(Player.Stocks);
    }

    public void SetPlayerName(string newName) {
        PlayerNameText.text = newName;
    }

    public void SetWalletAmount(int newAmount) {
        WalletAmountText.text = "$" + newAmount.ToString();
    }

    public void UpdatePlayerStocks(List<Stock> stocks) {
        NestorStockText.text = "NESTOR: " + stocks.FindAll(stock => stock.CorporationId.Equals(0)).Count.ToString();
        NestorStockText.text = "SPARK: " + stocks.FindAll(stock => stock.CorporationId.Equals(1)).Count.ToString();
        EtchStockText.text = "ETCH: " + stocks.FindAll(stock => stock.CorporationId.Equals(2)).Count.ToString();
        RoveStockText.text = "ROVE: " + stocks.FindAll(stock => stock.CorporationId.Equals(3)).Count.ToString();
        FleetStockText.text = "FLEET: " + stocks.FindAll(stock => stock.CorporationId.Equals(4)).Count.ToString();
        EchoStockText.text = "ECHO: " + stocks.FindAll(stock => stock.CorporationId.Equals(5)).Count.ToString();
        BoltStockText.text = "BOLT: " + stocks.FindAll(stock => stock.CorporationId.Equals(6)).Count.ToString();
    }

    public void AddPlayerTile(Player player, Tile tile) {
        GameObject newTile = Instantiate(tilePrefab);
        newTile.transform.SetParent(TileGrid);

        // Give the TileObject script the tile & player
        newTile.GetComponent<TileObject>().Tile = tile;
        newTile.GetComponent<TileObject>().Player = player;
        newTile.gameObject.name = tile.Id.ToString();

        SetTileText(newTile, tile.Letter, tile.Number);
    }

    public void SetPlayerTiles(Player player, List<Tile> tiles) {
        foreach (Tile tile in tiles) {
            GameObject newTile = Instantiate(tilePrefab);
            newTile.transform.SetParent(TileGrid, true);

            // Give the TileObject script the tile & player
            newTile.GetComponent<TileObject>().Tile = tile;
            newTile.GetComponent<TileObject>().Player = player;
            newTile.gameObject.name = tile.Id.ToString();

            SetTileText(newTile, tile.Letter, tile.Number);
        }
    }

    public void RemovePlayerTile(Player player, Tile tile) {
        GameObject tileToRemove = TileGrid.Find(tile.Id.ToString()).gameObject;
        Destroy(tileToRemove);

    }

    private void SetTileText(GameObject tile, string letter, string number) {
        Text[] tileText = tile.GetComponentsInChildren<Text>();
        foreach (Text text in tileText)
            if (text.name == "Letter")
                text.text = letter;
            else
                text.text = number;
    }
}
