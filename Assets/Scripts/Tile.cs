using UnityEngine;

public class Tile {

    #region Class Attributes

    private int _id;
    private string _name;

    #endregion

    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public Tile(int id, string name) {
        _id = id;
        _name = name;
    }

    ~Tile() {
        Debug.Log("Tile: " + Name + " removed from game.");
    }

}
