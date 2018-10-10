using System;
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
    private List<GameObject> _tiles = new List<GameObject>();
    private int _stockValue;
    // TODO: Add other stock value table elements here and update them with _stockValue
    private HashSet<StockValue> _stockValueTable = new HashSet<StockValue>();
    //private Dictionary<int, StockValue> _stockValueTable = new Dictionary<int, StockValue>();
    private bool _isSafe;

    #endregion Class Attributes

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

    public List<GameObject> Tiles {
        get { return _tiles; }
        set { _tiles = value; }
    }

    public int StockValue {
        get {
            //if (_stockValueTable.ContainsKey(_tileSize))
            //    _stockValue = _stockValueTable[_tileSize].Price;
            foreach (StockValue sv in _stockValueTable)
                if (_tileSize >= sv.MinSize && _tileSize <= sv.MaxSize)
                    _stockValue = sv.Price;

            return _stockValue;
        }
        //set {
        //    if (value < 0)
        //        _stockValue = 0;
        //    else
        //        _stockValue = value;
        //}
    }

    public HashSet<StockValue> StockValueTable {
        get { return _stockValueTable; }
        set { _stockValueTable = value; }
    }

    /// <summary>
    /// Checks and Sets if the Corporation is Safe
    /// </summary>
    public bool IsSafe {
        get {
            if (_tileSize >= Constants.NumberOfTilesForSafeCorporation)
                _isSafe = true;
            return _isSafe;
        }
        private set {
            if (!_isSafe) {
                _isSafe = value;
            }
        }
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
        //Debug.Log("Corporation: " + Name + " was removed from game.");
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

    #endregion Overrides
}