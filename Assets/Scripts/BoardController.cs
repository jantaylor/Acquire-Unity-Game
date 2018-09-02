using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public GameObject tile;
    private Board _board = new Board(10, 10);
    private Transform _boardObject;
    private Vector3[] _gridPositions = new Vector3[100];

    public void Awake() {
        //Creates the empty tiles
        //BoardSetup();
        InitializeTileList();
        _boardObject = GameObject.Find("Board").transform;
    }

    /// <summary>
    /// Create list of Tiles on the "game board"
    /// </summary>
    private void InitializeTileList() {
        int id = 0;
        for (int y = _board.Columns - 1; y > -1; --y) {
            for (int x = -1; x < _board.Rows -1; ++x) {
                _gridPositions[id] = new Vector3(x + .6f, y + .5f, 0f); // TODO: should be perfect
                ++id;
            }
        }
    }

    /// <summary>
    /// Get the position of the tile on the board by passing in the id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Vector3 GetTilePositionOnBoard(int id) {
        return _gridPositions[id];
    }

    /// <summary>
    /// Moves tile from HUD to game board
    /// </summary>
    /// <param name="tile"></param>
    public void PlaceTileOnBoard(GameObject tile) {
        // Set the parent of the new empty Tile to "Board"
        tile.transform.SetParent(_boardObject);
        tile.transform.localPosition = tile.GetComponent<TileObject>().Tile.Position;

        // Update board class with placed tiles and empty tiles
        _board.PlacedTiles[GameManager.Instance.turnNumber] = tile;
    }

    public void HighLightBoard(Tile tile) {
        // TODO: Highlight where on the board the tile would go
    }
}