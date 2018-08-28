using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stock class object so players can hold mulitple stock
/// </summary>
public class Stock {

    #region Class Attributes

    private int _corporationId;
    private string _corporationName;

    #endregion

    public int CorporationId {
        get { return _corporationId; }
        set { _corporationId = value; }
    }
    public string Corporation {
        get { return _corporationName; }
        set { _corporationName = value; }
    }

    public Stock() {

    }

    public Stock(int corporationId, string corporationName) {
        _corporationId = corporationId;
        _corporationName = corporationName;
    }

    ~Stock() {
        Debug.Log("Stock: " + _corporationId + " " + _corporationName + " removed from game.");
    }
}
