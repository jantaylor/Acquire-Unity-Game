using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stock class object so players can hold mulitple stock
/// </summary>
public class Stock {

    #region Class Attributes

    private Corporation _corporation;

    #endregion

    public Corporation Corporation {
        get { return _corporation; }
        set { _corporation = value; }
    }

    public Stock(Corporation corporation) {
        _corporation = corporation;
    }

    ~Stock() {
        Debug.Log("Stock: " + _corporation.Name + " removed from game.");
    }
}
