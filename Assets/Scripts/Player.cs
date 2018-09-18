using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player class holds the player's information that belongs to them
/// </summary>
public class Player {

    #region Class Attributes

    private int _id;
    private string _name;
    private string _color;
    private List<Stock> _stocks = new List<Stock>();
    private List<Tile> _tiles = new List<Tile>();

    #endregion

    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public string Color {
        get { return _color; }
        set { _color = value; }
    }

    public List<Stock> Stocks {
        get { return _stocks; }
        set { _stocks = value; }
    }

    public List<Tile> Tiles {
        get { return _tiles; }
        set { _tiles = value; }
    }

    public Player() {

    }

    public Player(int id, string name, string color) {
        _id = id;
        _name = name;
        _color = color;
    }

    ~Player() {
        //Debug.Log("Player: " + Name + " removed from the game.");
    }

}
