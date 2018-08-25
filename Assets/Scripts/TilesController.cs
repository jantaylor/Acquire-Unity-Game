using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    // Set class attributes
    #region Class Attributes

    private int _id;
    private string _name;

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

    #endregion
}

public class TilesController : MonoBehaviour {

    #region Class Imports

    #endregion

    // Set class attributes
    #region Class Attributes

    private Tile[] tiles;

    enum Letter { A, B, C, D, E, F, G, H, I, J };

    #endregion

    #region Unity Specific

    // Use this for initialization
    void Start () {
        tiles = new Tile[100];
        Create();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #endregion

    #region Class Functions

    /// <summary>
    /// Create a tile array of 100 tiles rather than a 2D array of 10, 10.
    /// </summary>
    public void Create() {
        int letter = 0;
        int number = 0;
        for (int i = 0; i < tiles.Length; ++i) {
            tiles[i] = new Tile(i, GetLetterFromInt(letter) + (number + 1).ToString());
            number++;
            if (number >= 9)
                letter++;
        }
    }

    /// <summary>
    /// Supply an enum int-value and get the string-value
    /// </summary>
    /// <param name="id">int value</param>
    /// <returns>string value</returns>
    public string GetLetterFromInt(int id) {
        Letter letter = (Letter)id;
        return letter.ToString();
    }

    /// <summary>
    /// Print a list of the tiles left
    /// </summary>
    public void PrintAvailableTiles() {
        foreach (Tile tile in tiles)
            Debug.Log("Tile: " + tile.Name);
    }

    /// <summary>
    /// Return the number of tiles left, what's remaining in the tiles array
    /// </summary>
    /// <returns></returns>
    public int AvailableTilesLeft() {
        return tiles.Length;
    }

    /// <summary>
    /// Shuffles the array of tiles using CommonFunctions
    /// </summary>
    public void Shuffle() {
        CommonFunctions.Shuffle<Tile>(new System.Random(), tiles);
    }

    public Tile Draw() {
        // TODO: refactor tiles to be Stack or Queue instead
        return tiles[0];
    }

    public void ReturnTile(Tile tile) {
        // TODO: return the tile to the stack/queue
    }

    /// <summary>
    /// Take the old tile and replace with the next tile
    /// </summary>
    /// <param name="oldTile">Tile being traded in</param>
    /// <returns>New Tile from pile, never oldTile</returns>
    public Tile Replace(Tile oldTile) {
        Tile newTile = Draw();
        ReturnTile(oldTile);
        Shuffle();
        return newTile;
    }

    public void Place(Tile tile) {
        // TODO: Place the tile on the board
    }

    public void HighLightBoard(Tile tile) {
        // TODO: Highlight where on the board the tile would go
    }

    #endregion
}
