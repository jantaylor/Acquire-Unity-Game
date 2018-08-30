using UnityEngine;

public class Tile {

    #region Class Attributes

    private int _id;
    private string _number;
    private string _letter;
    private Corporation _corporation;

    #endregion

    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Number {
        get { return _number; }
        set { _number = value; }
    }

    public string Letter {
        get { return _letter; }
        set { _letter = value; }
    }

    public Corporation Corporation {
        get { return _corporation; }
        set { _corporation = value; }
    }

    public Tile() {

    }

    public Tile(int id, string number, string letter, Corporation corporation = null) {
        _id = id;
        _number = Number;
        _letter = letter;
        _corporation = corporation;
    }

    ~Tile() {
        Debug.Log("Tile: " + _number + _letter + " removed from game.");
    }

}
