using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Corporation class holds all the corporation deatils
/// </summary>
public class Corporation {

    #region Class Attributes

    private int _id;
    private string _name;
    private Stack<Stock> _stocks = new Stack<Stock>();
    private int _tileSize;
    private int _stockValue;
    private bool _isSafe;

    #endregion

    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public Stack<Stock> Stocks {
        get { return _stocks; }
        set { _stocks = value; }
    }

    public int TileSize {
        get { return _tileSize; }
        set {
            if (value < 0)
                _tileSize = 0;
            else
                _tileSize = value;
        }
    }

    public int StockValue {
        get { return _stockValue; }
        set {
            if (value < 0)
                _stockValue = 0;
            else
                _stockValue = value;
        }
    }

    public bool IsSafe {
        get { return _isSafe; }
        set { _isSafe = value; }
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Corporation() {

    }

    public Corporation(int id, string name, int tileSize = 0, int stockValue = 0, bool isSafe = false) {
        _id = id;
        _name = name;
        _tileSize = tileSize;
        _stockValue = stockValue;
        _isSafe = isSafe;
    }

    ~Corporation() {
        Debug.Log("Corporation: " + Name + " was removed from game.");
    }

    #region Overrides

    //public override bool Equals(System.Object obj) {
    //    //Check for null and compare run-time types.
    //    if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
    //        return false;
    //    } else {
    //        Corporation c = (Corporation)obj;
    //        return (_id == c.Id) && (_name == c.Name);
    //    }
    //}

    #endregion
}
