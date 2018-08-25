using UnityEngine;

public class Tile {

    #region Class Attributes

    private int _id;
    private string _name;
    private Corporation _corporation;

    #endregion

    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public Corporation Corporation {
        get { return _corporation; }
        set { _corporation = value; }
    }

    public Tile(int id, string name, Corporation corporation = null) {
        _id = id;
        _name = name;
        _corporation = corporation;
    }

    ~Tile() {
        Debug.Log("Tile: " + Name + " removed from game.");
    }

}
