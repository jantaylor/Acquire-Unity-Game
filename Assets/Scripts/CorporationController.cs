using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporationController : MonoBehaviour {

    #region Class Imports

    #endregion

    // Set class attributes
    #region Class Attributes

    private Corporation[] _corporations;

    #endregion

    #region Unity Specific

    void Start() {
        _corporations = new Corporation[7];
        BuildCorporations();
    }

    #endregion

    #region Class Functions

    /// <summary>
    /// Build out all 6 corporations
    /// </summary>
    public void BuildCorporations() {
        _corporations[0] = new Corporation(0, "Nestor");
        _corporations[1] = new Corporation(1, "Spark");
        _corporations[2] = new Corporation(2, "Etch");
        _corporations[3] = new Corporation(3, "Rove");
        _corporations[4] = new Corporation(4, "Fleet");
        _corporations[5] = new Corporation(5, "Bolt");
        _corporations[6] = new Corporation(6, "Echo");
    }

    //TODO: change return type
    public void OptionsToBuy() {
        foreach (Corporation corp in _corporations) {
            Debug.Log("Corporation: " + corp.Name + " Available Stocks: " + corp.StockAvailable);
        }
    }

    /// <summary>
    /// Buy # stock in the passed Corporation id
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <param name="amount">Amount to buy 1 or 2</param>
    public void BuyStock(int id, int amount = 1) {
        if (amount > 2) {
            Debug.LogError("Tried to buy more than 2 stocks - that's cheating.");
            throw new System.Exception("Unable to purchase more than 2 stocks a turn.");
        }

        if (_corporations[id].StockAvailable >= amount)
            _corporations[id].StockAvailable -= amount;

        // TODO return something or error handling
    }

    /// <summary>
    /// Sell # stock in the passed Corporation id
    /// </summary>
    /// <param name="id">Corporation ID</param>
    /// <param name="amount">Amount to sell</param>
    public void SellStock(int id, int amount) {
        _corporations[id].StockAvailable += amount;
        // TODO return something or error handling
    }

    /// <summary>
    /// Trade # of stock from first corp id to new corp id. Get half back.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <param name="newId"></param>
    public void TradeStock(int id, int amount, int newId) {
        _corporations[id].StockAvailable += amount;

        if (_corporations[newId].StockAvailable >= (amount / 2))
            _corporations[newId].StockAvailable -= (amount / 2);
        else
            _corporations[newId].StockAvailable -= _corporations[newId].StockAvailable;

        // TODO return something or error handling
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
        return _corporations[id].StockAvailable;
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
        BuildCorporations();
    }

    #endregion
}