using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporationController : MonoBehaviour {

    // Set class attributes
    #region Class Attributes

    private List<Corporation> _corporations = new List<Corporation>();

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
        _corporations.Add(NewCorporation(0, "Nestor"));
        _corporations.Add(NewCorporation(1, "Spark"));
        _corporations.Add(NewCorporation(2, "Etch"));
        _corporations.Add(NewCorporation(3, "Rove"));
        _corporations.Add(NewCorporation(4, "Fleet"));
        _corporations.Add(NewCorporation(5, "Bolt"));
        _corporations.Add(NewCorporation(6, "Echo"));
    }

    /// <summary>
    /// Create a new corporation and build a stack of 24 stocks
    /// </summary>
    /// <param name="id">Corporation's Id</param>
    /// <param name="name">Corporation's Name</param>
    /// <returns></returns>
    private Corporation NewCorporation(int id, string name) {
        Corporation newCorp = new Corporation(id, name);
        for (int i = 0; i < 24; ++i)
            newCorp.Stocks.Push(new Stock(id, name));

        return newCorp;
    }

    public Corporation Corporation(int id) {
        return _corporations.Find(c => c.Id.Equals(id));
    }

    //TODO: change return type
    public void OptionsToBuy() {
        foreach (Corporation corp in _corporations) {
            Debug.Log("Corporation: " + corp.Name + " Available Stocks: " + corp.Stocks.Count);
        }
    }

    /// <summary>
    /// Buy # stock in the passed Corporation id
    /// TODO: Overload this with int[] id so that player can buy multiple at a time
    /// </summary>
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
            for (int i = 0; i < amount; ++i)
                player.Stocks.Add(corp.Stocks.Pop());

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
            _corporations[id].Stocks.Push(sellingStock[i]);

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
        if (_corporations[newStockId].Stocks.Count >= tradeAmount / 2) {
            for (int i = 0; i < tradeAmount; ++i)
                _corporations[tradeStockId].Stocks.Push(tradingStock[i]);

            Stock[] newStocks = new Stock[newAmount];
            for (int i = 0; i < tradingStock.Length / 2; ++i)
                newStocks[i] = _corporations[newStockId].Stocks.Pop();

            return newStocks;
        }

        throw new System.Exception("Something went wrong, there were not enough stocks left.");
    }

    /// <summary>
    /// Value of corp passed in
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <returns></returns>
    public int Value(int id) {
        return _corporations[id].StockValue;
    }

    /// <summary>
    /// Number of stocks a company has available
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <returns></returns>
    public int Available(int id) {
        return _corporations[id].Stocks.Count;
    }

    /// <summary>
    /// Make company's tilesize & stockvalue 0
    /// </summary>
    /// <param name="id">Corporation ID</param>
    public void MakeDefunct(int id) {
        _corporations[id].TileSize = 0;
        _corporations[id].StockValue = 0;
    }

    /// <summary>
    /// The number of tiles a company has - their size
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <returns></returns>
    public int Size(int id) {
        return _corporations[id].TileSize;
    }

    /// <summary>
    /// Increases the company's size
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <param name="tiles">Number of new Tiles</param>
    public void IncreaseSize(int id, int tiles) {
        _corporations[id].TileSize += tiles;
    }

    /// <summary>
    /// Is the corporation safe from a merger, bigger than 11 tile size
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <returns></returns>
    public bool IsSafe(int id) {
        return _corporations[id].IsSafe;
    }

    /// <summary>
    /// Since Build Corporations writes over the existing array, we will just do that again
    /// </summary>
    public void ResetCorporations() {
        _corporations = null;
        BuildCorporations();
    }

    #endregion
}