using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    #region Class Attributes

    private int _columns;
    private int _rows;
    private GameObject[] _emptyTiles = new GameObject[100];
    private GameObject[] _placedtiles = new GameObject[100];
    private GameObject[] _corpBuildingTiles = new GameObject[7];

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

    public GameObject[] CorpBuildingTiles {
        get { return _corpBuildingTiles; }
        set { _corpBuildingTiles = value; }
    }

    public Board(int columns = 10, int rows = 10) {
        _columns = columns;
        _rows = rows;
    }

    ~Board() {
        Debug.Log("Removed board.");
    }

}
