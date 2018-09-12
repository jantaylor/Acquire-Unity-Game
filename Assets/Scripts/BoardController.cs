using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    private Board _board = new Board(10, 10);
    private Transform _boardObject;
    private Color _green = new Color(0.32f, 0.85f, .3f, 0.9f);
    private Color _yellow = new Color(1, 1, 0.4f, 0.9f);
    private Vector3[] _gridPositions = new Vector3[100];

    public GameObject tile;

    public void Awake() {
        //Creates the empty tiles
        //BoardSetup();
        InitializeTileList();
        _boardObject = GameObject.Find("Board").transform;
    }

    IEnumerator ChangeTileColorAfterSeconds(GameObject tile, Color color, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        tile.GetComponent<SpriteRenderer>().color = color;
    }

    /// <summary>
    /// Create list of Tiles on the "game board"
    /// </summary>
    private void InitializeTileList() {
        // TODO: Set 0 (10) to be on right side...
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
        tile.GetComponent<SpriteRenderer>().color = _green;

        StartCoroutine(ChangeTileColorAfterSeconds(tile, Color.white, .3f));

        // Set the parent of the new empty Tile to "Board"
        tile.transform.SetParent(_boardObject);
        tile.transform.localPosition = tile.GetComponent<TileObject>().Tile.Position;

        // Update board class with placed tiles and empty tiles
        _board.PlacedTiles[GameManager.Instance.TurnNumber] = tile;
    }

    public void HighlightBoard(GameObject highlight, GameObject tile) {
        highlight.GetComponent<SpriteRenderer>().color = _yellow;
        highlight.transform.SetParent(_boardObject);
        highlight.transform.localPosition = tile.GetComponent<TileObject>().Tile.Position;
    }
}