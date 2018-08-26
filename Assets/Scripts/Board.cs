using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    #region Class Attributes

    private int _columns;
    private int _rows;
    private GameObject[] _emptyTiles;
    private GameObject[] _placedtiles;
    private GameObject[] _corporationTiles;

    #endregion

    public int Columns {
        get { return _columns; }
        set { _columns = value; }
    }

    public int Rows {
        get { return _rows; }
        set { _rows = value; }
    }

    public GameObject[] EmptyTiles {
        get { return _emptyTiles; }
        set { _emptyTiles = value; }
    }

    public GameObject[] PlacedTiles {
        get { return _placedtiles; }
        set { _placedtiles = value; }
    }

    public GameObject[] CorporationTiles {
        get { return _corporationTiles; }
        set { _corporationTiles = value; }
    }

    public Board(int columns = 10, int rows = 10) {
        _columns = columns;
        _rows = rows;
    }

    ~Board() {
        Debug.Log("Removed board.");
    }

}
