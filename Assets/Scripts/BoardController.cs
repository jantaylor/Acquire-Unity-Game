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
            for (int x = 0; x < _board.Rows; ++x) {
                _gridPositions[id] = new Vector3(x + .6f, y + .5f); // TODO: should be perfect
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
    public void PlaceTileOnBoard(GameObject tile, bool placeInstantly = false) {
        // Skip the yellow/green delay
        if (!placeInstantly) {
            tile.GetComponent<SpriteRenderer>().color = _green;
            StartCoroutine(ChangeTileColorAfterSeconds(tile, Color.white, .3f));
            GameManager.Instance.TilePlaced = true;
        }
        // Set the parent of the new empty Tile to "Board"
        tile.transform.SetParent(_boardObject);
        tile.transform.localPosition = tile.GetComponent<TileObject>().Tile.Position;

        // Update board class with placed tiles and empty tiles
        _board.PlacedTiles[GameManager.Instance.TurnNumber] = tile;

        // Check for adjacent tiles
        CheckForAdjacentTiles(tile);
    }

    public void HighlightBoard(GameObject highlight, GameObject tile) {
        highlight.GetComponent<SpriteRenderer>().color = _yellow;
        highlight.transform.SetParent(_boardObject);
        highlight.transform.localPosition = tile.GetComponent<TileObject>().Tile.Position;
    }

    public void CheckForAdjacentTiles(GameObject placedTile) {
        Corporation placedTileCorp = placedTile.GetComponent<TileObject>().Tile.Corporation;
        int layerMask = LayerMask.GetMask("Tile");
        Collider2D tileCollider = placedTile.GetComponent<Collider2D>();
        RaycastHit2D tempTileHit;
        List<GameObject> tilesHit = new List<GameObject>();
        Corporation tileHitCorporation;

        // Raycast from the tile and see if it hits an adacent tile up, down, left, right
        tempTileHit = Physics2D.Raycast(transform.position, Vector2.up, tileCollider.bounds.size.y, layerMask);
        if (tempTileHit) tilesHit.Add(tempTileHit.transform.parent.gameObject);

        tempTileHit = Physics2D.Raycast(transform.position, -Vector2.up, tileCollider.bounds.size.y, layerMask);
        if (tempTileHit) tilesHit.Add(tempTileHit.transform.parent.gameObject);

        tempTileHit = Physics2D.Raycast(transform.position, Vector2.left, tileCollider.bounds.size.x, layerMask);
        if (tempTileHit) tilesHit.Add(tempTileHit.transform.parent.gameObject);

        tempTileHit = Physics2D.Raycast(transform.position, Vector2.right, tileCollider.bounds.size.x, layerMask);
        if (tempTileHit) tilesHit.Add(tempTileHit.transform.parent.gameObject);

        if (tilesHit.Count > 0) Debug.Log(tilesHit.Count.ToString() + "Tiles Hit!");
        // Loop through list of RayCast2D hit tiles and check for tiles with corporations and without
        // TODO: Figure out 3-4 way Merger
        foreach (GameObject tileHit in tilesHit) {
            tileHitCorporation = tileHit.GetComponent<TileObject>().Tile.Corporation;
            if (tileHitCorporation && placedTileCorp) {
                // If hit tile is part of corporation and we are part of a corporation, then MERGER
                if (tileHitCorporation.TileSize > placedTileCorp.TileSize) {
                    // TileHitCorporation survives merger
                    if (!placedTileCorp.IsSafe)
                        GameManager.Instance.CorporationController.MergeCorporations(tileHitCorporation, placedTileCorp);
                } else if (tileHitCorporation.TileSize == placedTileCorp.TileSize) {
                    // TileHitCorporation and placedTileCorp are same, players choice on who survives
                    // TODO: Players choice, for now placed tile
                    if (!tileHitCorporation.IsSafe)
                        GameManager.Instance.CorporationController.MergeCorporations(placedTileCorp, tileHitCorporation);
                } else {
                    // PlacedtileCorp survives merger
                    if (!tileHitCorporation.IsSafe)
                        GameManager.Instance.CorporationController.MergeCorporations(placedTileCorp, tileHitCorporation);
                }
            } else if (tileHitCorporation && !placedTileCorp) {
                // If hit tile is part of a corporation and the placed tile is not, then add it to existing corporation
                // Set placed tile corp to hit corp
                placedTileCorp = tileHitCorporation;
                GameManager.Instance.CorporationController.IncreaseSize(tileHitCorporation, 1, placedTile);
            } else {
                // If both tiles are not part of corporation, then found a corporation!
                // TODO: Players Choice, for now random
                placedTileCorp = GameManager.Instance.CorporationController.RandomCorporation();
                tileHitCorporation = placedTileCorp;
            }
        }

        tilesHit = null; // Destroy List
    }
}