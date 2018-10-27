using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    #region Class Imports

    #endregion

    // Set class attributes
    #region Class Attributes

    private Queue<Tile> _tiles;
    public GameObject tilePrefab;

    public Queue<Tile> Tiles {
        get { return _tiles; }
    }

    enum Letter { A, B, C, D, E, F, G, H, I, J };

    #endregion

    #region Unity Specific

    // Use this for initialization
    void Start () {
        _tiles = new Queue<Tile>();
        CreatePile();
	}

    #endregion

    #region Class Functions

    /// <summary>
    /// Create a tile array of 100 tiles rather than a 2D array of 10, 10 and then shuffles it.
    /// </summary>
    public void CreatePile() {
        int id = 0;
        for (int x = 0; x < 10; ++x) {
            // Loop along y axis
            for (int y = 1; y < 11; ++y) {
                if (y == 10) y = 0;
                Tile newTile = new Tile(id, y.ToString(), GetLetterFromInt(x), GameManager.Instance.BoardController.GetTilePositionOnBoard(id));
                _tiles.Enqueue(newTile);
                if (y == 0) y = 10;
                ++id;
            }
        }

        ShufflePile();
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
    public void PrintPile() {
        // TODO: Print to somewhere or return string
        foreach (Tile tile in _tiles)
            Debug.Log("Tile: " + tile.Number + tile.Letter);
    }

    /// <summary>
    /// Print the tile information
    /// </summary>
    /// <param name="tile"></param>
    public void PrintTile(Tile tile) {
        Debug.Log("Tile ID: " + tile.Id + ", Text: " + tile.Number + tile.Letter + ", Corp: " + tile.Corporation + ", Position: " + tile.Position);
    }

    /// <summary>
    /// Return the number of tiles left, what's remaining in the tiles array
    /// </summary>
    /// <returns>Count of tiles queue</returns>
    public int LeftInPile() {
        return _tiles.Count;
    }

    /// <summary>
    /// Shuffles the array of tiles using CommonFunctions
    /// </summary>
    public void ShufflePile() {
        // TODO: Look into using a List instead or benchmark this
        Tile[] tileArray = _tiles.ToArray();
        _tiles.Clear();
        CommonFunctions.Shuffle<Tile>(new System.Random(), tileArray);
        foreach (Tile tile in tileArray)
            _tiles.Enqueue(tile);
    }

    /// <summary>
    /// Draws a tile from the queue of available tiles
    /// </summary>
    /// <returns>First tile off queue</returns>
    public Tile DrawTile() {
        return _tiles.Dequeue();
    }

    /// <summary>
    /// Take the old tile and replace with the next tile
    /// </summary>
    /// <param name="oldTile">Tile being traded in</param>
    /// <returns>New Tile from pile, never oldTile</returns>
    public Tile TradeTile(Tile oldTile) {
        Tile newTile = DrawTile();
        _tiles.Enqueue(oldTile);
        ShufflePile();
        return newTile;
    }

    public GameObject CreateTileObject(Tile tile, Vector3 position) {
        GameObject newTile = Instantiate(tilePrefab);

        // Give the TileObject script the tile
        newTile.GetComponent<TileObject>().Tile = tile;

        // Move the tile to it's position
        newTile.transform.position = position;

        // Set the tile's text
        SetTileText(newTile, tile.Letter, tile.Number);

        // Set the Tile's name
        newTile.name = "Tile " + tile.Letter + tile.Number + " (" + tile.Id + ")";

        return newTile;
    }

    public void SetTileCorporation(GameObject tileObject, Corporation corporation) {
        Tile tile = tileObject.GetComponent<TileObject>().Tile;
        tile.Corporation = corporation;
        GameManager.Instance.CorporationController.IncreaseSize(tile.Corporation, 1,tileObject);
        GameManager.Instance.Game.Log("Tile " + tile.Number + tile.Letter + " now belongs to " + tile.Corporation.Name);

    }

    /// <summary>
    /// // set number and letter of tile
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="letter"></param>
    /// <param name="number"></param>
    public void SetTileText(GameObject tile, string letter, string number) {
        
        TextMesh[] tileText = tile.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh textMesh in tileText)
            if (textMesh.name == "Letter")
                textMesh.text = letter;
            else
                textMesh.text = number;
    }

    #endregion
}
