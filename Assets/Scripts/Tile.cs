﻿using UnityEngine;

public class Tile {

    #region Class Attributes

    private int _id;
    private string _number;
    private string _letter;
    private Vector3 _position;

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

    public Corporation Corporation { get; set; }

    public Vector3 Position {
        get { return _position; }
        set { _position = value; }
    }

    public Tile() {

    }

    public Tile(int id, string number, string letter, Vector3 position = new Vector3()) {
        _id = id;
        _number = number;
        _letter = letter;
        _position = position;
    }

    ~Tile() {
        //Debug.Log("Tile: " + _number + _letter + " removed from game.");
    }

}
