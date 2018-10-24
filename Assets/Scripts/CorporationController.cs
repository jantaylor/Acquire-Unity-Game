using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporationController : MonoBehaviour {

    // Set class attributes
    #region Class Attributes
    public StockValue StockValue;
    public List<Corporation> Corporations = new List<Corporation>();

    #endregion

    #region Unity Specific

    private void Awake() {
        BuildCorporations();
    }

    #endregion

    #region Class Functions

    /// <summary>
    /// Build out all 6 corporations
    /// </summary>
    public void BuildCorporations() {
        Corporations.Add(NewCorporation(0, "Nestor"));
        Corporations.Add(NewCorporation(1, "Spark"));
        Corporations.Add(NewCorporation(2, "Etch"));
        Corporations.Add(NewCorporation(3, "Rove"));
        Corporations.Add(NewCorporation(4, "Fleet"));
        Corporations.Add(NewCorporation(5, "Bolt"));
        Corporations.Add(NewCorporation(6, "Echo"));
    }

    /// <summary>
    /// Create a new corporation and build a stack of 24 stocks
    /// </summary>
    /// <param name="id">Corporation's Id</param>
    /// <param name="name">Corporation's Name</param>
    /// <returns></returns>
    private Corporation NewCorporation(int id, string name) {
        Corporation newCorp = new Corporation(id, name);
        for (int i = 0; i < Constants.NumberOfStocksPerCorporation; ++i)
            newCorp.Stocks.Push(new Stock(id, name));

        GenerateStockValueTable(id, newCorp.StockValueTable);
        return newCorp;
    }

    public Corporation Corporation(int id) {
        return Corporations.Find(c => c.Id.Equals(id));
    }

    public Corporation[] Corporation() {
        return Corporations.ToArray();
    }

    /// <summary>
    /// Buy # stock in the passed Corporation id
    /// TODO: Overload this with int[] id so that player can buy multiple at a time
    /// </summary>
    /// <param name="player">Player buying (ActivePlayer)</param>
    /// <param name="id">Corporation ID</param>
    /// <param name="amount">Amount to buy 1 or 2</param>
    public void BuyStock(Player player, int id, int amount = 1) {
        if (amount > 3) {
            Debug.Log("Tried to buy more than 3 stocks - that's cheating.");
            throw new System.Exception("Unable to purchase more than 3 stocks a turn.");
        }

        Corporation corp = Corporation(id);
        //Debug.Log("Player: " + player.Name + " is " + " buying " + amount.ToString() + " " + corp.Name +" stocks when " + corp.Name + " has " + corp.Stocks.Count + " stocks available");

        if (corp.Stocks.Count >= amount) {
            for (int i = 0; i < amount; ++i) {
                player.Stocks.Add(corp.Stocks.Pop());
            }

            ++GameManager.Instance.StocksPurchased;
            GameManager.Instance.MoneyController.SpendMoney(player, corp.StockValue);
            Debug.Log("Successfully bought stock, player's stock count: " + player.Stocks.Count.ToString());
            return;
        }

        throw new System.Exception("Something went wrong, there were no more stocks left.");
    }

    /// <summary>
    /// Sell # stock in the passed Corporation id
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <param name="amount">Amount to sell</param>
    public int SellStock(int id, Stock[] sellingStock) {
        for (int i = 0; i < sellingStock.Length; ++i)
            Corporations[id].Stocks.Push(sellingStock[i]);

        // TODO: Look up and return based on value of stock
        return 0;
    }

    /// <summary>
    /// Trade # of stock from first corp id to new corp id. Get half back.
    /// </summary>
    /// <param name="tradeStockId">Trading stock corporation Id</param>
    /// <param name="tradingStock">Array of stock trading in, pass in multiples of 2</param>
    /// <param name="newStockId">New corporation stock Id</param>
    public Stock[] TradeStock(int tradeStockId, Stock[] tradingStock, int newStockId) {
        int tradeAmount = tradingStock.Length;
        int newAmount = tradeAmount / 2;
        if (Corporations[newStockId].Stocks.Count >= tradeAmount / 2) {
            for (int i = 0; i < tradeAmount; ++i)
                Corporations[tradeStockId].Stocks.Push(tradingStock[i]);

            Stock[] newStocks = new Stock[newAmount];
            for (int i = 0; i < tradingStock.Length / 2; ++i)
                newStocks[i] = Corporations[newStockId].Stocks.Pop();

            return newStocks;
        }

        throw new System.Exception("Something went wrong, there were not enough stocks left.");
    }

    /// <summary>
    /// Value of corp passed in
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <returns></returns>
    //public int Value(int id) {
    //    foreach (StockValue sv in Corporations[id].StockValueTable) {
    //        if (Corporations[id].TileSize >= sv.MinSize && Corporations[id].TileSize <= sv.MaxSize) {
    //            Corporations[id].StockValue = sv.Price;
    //        }
    //    }

    //    return Corporations[id].StockValue;
    //}

    /// <summary>
    /// Number of stocks a company has available
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <returns></returns>
    public int Available(int id) {
        return Corporations[id].Stocks.Count;
    }

    public List<Corporation> AvailableToFound() {
        List <Corporation> availableList = new List<Corporation>();
        foreach (Corporation corp in Corporations)
            if (corp.TileSize == 0)
                availableList.Add(corp);
        return availableList;
    }

    /// <summary>
    /// Make Corporation's tilesize & stockvalue 0
    /// </summary>
    /// <param name="id">Corporation ID</param>
    public void MakeDefunct(int id) {
        Corporations[id].Tiles = null;
    }

    /// <summary>
    /// Make Corporation's tilesize & stockvalue 0
    /// </summary>
    /// <param name="id">Corporation</param>
    public void MakeDefunct(Corporation corporation) {
        corporation.Tiles = null;
    }

    /// <summary>
    /// The number of tiles a company has - their size
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <returns></returns>
    public int Size(int id) {
        return Corporations[id].TileSize;
    }

    /// <summary>
    /// Increases the company's size
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <param name="tiles">Number of new Tiles</param>
    public void IncreaseSize(Corporation corporation, int tiles, GameObject tile) {
        corporation.Tiles.Add(tile);
    }

    /// <summary>
    /// Merge the Corporations - Check if Defunct Corp is safe first
    /// </summary>
    /// <param name="corporation">Surviving Corp</param>
    /// <param name="defunctCorp">Corp becoming Defunct</param>
    public void MergeCorporations(Corporation corporation, Corporation defunctCorp) {
        try {
            if (defunctCorp.IsSafe)
                throw new System.Exception("Invalid Merge, defunctCorp is Safe from mergers.");

            foreach (GameObject tile in defunctCorp.Tiles) {
                corporation.Tiles.Add(tile);
                defunctCorp.Tiles.Remove(tile);
            }
            MakeDefunct(defunctCorp);
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// Since Build Corporations writes over the existing array, we will just do that again
    /// </summary>
    public void ResetCorporations() {
        Corporations = null;
        BuildCorporations();
    }

    /// <summary>
    /// Get a random Corporation
    /// </summary>
    /// <returns>Corporation</returns>
    public Corporation RandomCorporation() {
        List<Corporation> tempList = AvailableToFound();
        return tempList.RandomElement();
    }

    #endregion

    #region StockValueTable

    private void GenerateStockValueTable(int corpId, HashSet<StockValue> stockTable) {
        // 0-1, 2-4, and 5-6 have the same values - just different corporations
        switch (corpId) {
            case 0:
            case 1:
                stockTable.Add(new StockValue(2, 2, corpId, 200, 2000, 1500, 1000));
                stockTable.Add(new StockValue(3, 3, corpId, 300, 3000, 2200, 1500));
                stockTable.Add(new StockValue(4, 4, corpId, 400, 4000, 3000, 2000));
                stockTable.Add(new StockValue(5, 5, corpId, 500, 5000, 3700, 2500));
                stockTable.Add(new StockValue(6, 7, corpId, 600, 6000, 4200, 3000));
                stockTable.Add(new StockValue(8, 17, corpId, 700, 7000, 5000, 3500));
                stockTable.Add(new StockValue(18, 27, corpId, 800, 8000, 5700, 4000));
                stockTable.Add(new StockValue(28, 37, corpId, 900, 9000, 6200, 4500));
                stockTable.Add(new StockValue(38, 100, corpId, 1000, 10000, 7000, 5000));
                break;

            case 2:
            case 3:
            case 4:
                stockTable.Add(new StockValue(2, 2, corpId, 300, 3000, 2200, 1500));
                stockTable.Add(new StockValue(3, 3, corpId, 400, 4000, 3000, 2000));
                stockTable.Add(new StockValue(4, 4, corpId, 500, 5000, 3700, 2500));
                stockTable.Add(new StockValue(5, 5, corpId, 600, 6000, 4200, 3000));
                stockTable.Add(new StockValue(6, 7, corpId, 700, 7000, 5000, 3500));
                stockTable.Add(new StockValue(8, 17, corpId, 800, 8000, 5700, 4000));
                stockTable.Add(new StockValue(18, 27, corpId, 900, 9000, 6200, 4500));
                stockTable.Add(new StockValue(28, 37, corpId, 1000, 10000, 7000, 5000));
                stockTable.Add(new StockValue(38, 100, corpId, 1100, 11000, 7700, 5500));
                break;

            case 5:
            case 6:
                stockTable.Add(new StockValue(2, 2, corpId, 400, 4000, 3000, 2000));
                stockTable.Add(new StockValue(3, 3, corpId, 500, 5000, 3700, 2500));
                stockTable.Add(new StockValue(4, 4, corpId, 600, 6000, 4200, 3000));
                stockTable.Add(new StockValue(5, 5, corpId, 700, 7000, 5000, 3500));
                stockTable.Add(new StockValue(6, 7, corpId, 800, 8000, 5700, 4000));
                stockTable.Add(new StockValue(8, 17, corpId, 900, 9000, 6200, 4500));
                stockTable.Add(new StockValue(18, 27, corpId, 1000, 10000, 7000, 5000));
                stockTable.Add(new StockValue(28, 37, corpId, 1100, 11000, 7700, 5500));
                stockTable.Add(new StockValue(38, 100, corpId, 1200, 12000, 8200, 6000));
                break;

            default:
                break;
        }
    }

    #endregion
}