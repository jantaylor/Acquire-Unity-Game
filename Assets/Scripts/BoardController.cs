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

    private IEnumerator ChangeTileColorAfterSeconds(GameObject tile, Color color, float waitTime) {
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

        // Check for adjacent tiles, unless it's the first draw & place of the game
        if (!placeInstantly)
            CheckForAdjacentTiles(tile);
    }

    public void HighlightBoard(GameObject highlight, GameObject tile) {
        highlight.GetComponent<SpriteRenderer>().color = _yellow;
        highlight.transform.SetParent(_boardObject);
        highlight.transform.localPosition = tile.GetComponent<TileObject>().Tile.Position;

        // Debug to make sure Raycasts are working
        //RayCastFromTile(highlight, "up");
        //RayCastFromTile(highlight, "down");
        //RayCastFromTile(highlight, "left");
        //RayCastFromTile(highlight, "right");
    }

    // Remove Debugging
    public GameObject RayCastFromTile(GameObject tile, string dir) {
        Collider2D collider = tile.GetComponent<Collider2D>();
        int layerMask = LayerMask.GetMask("Tile");
        RaycastHit2D hit;

        if (dir.ToLower() == "up") {
            Debug.DrawRay(tile.transform.position, Vector2.up, Color.red, 1);
            hit = Physics2D.Raycast(tile.transform.position, Vector2.up, collider.bounds.size.y, layerMask);
        } else if (dir.ToLower() == "down") {
            Debug.DrawRay(tile.transform.position, -Vector2.up, Color.red, 1);
            hit = Physics2D.Raycast(tile.transform.position, -Vector2.up, collider.bounds.size.y, layerMask);
        } else if (dir.ToLower() == "left") {
            Debug.DrawRay(tile.transform.position, Vector2.left, Color.red, 1);
            hit = Physics2D.Raycast(tile.transform.position, Vector2.left, collider.bounds.size.x, layerMask);
        } else {
            Debug.DrawRay(tile.transform.position, Vector2.right, Color.red, 1);
            hit = Physics2D.Raycast(tile.transform.position, Vector2.right, collider.bounds.size.x, layerMask);
        }

        if (hit.collider != null) {
            Debug.Log("Hit a tile - " + hit.transform.gameObject.name);
            return hit.transform.gameObject;
        }

        //Debug.Log("No tiles hit");
        return null;
    }

    /// <summary>
    /// Check for tiles around the placed tile to create/merge a corporation
    /// </summary>
    /// <param name="placedTile">Tile GameObject</param>
    public void CheckForAdjacentTiles(GameObject placedTile) {
        Corporation placedTileCorp = placedTile.GetComponent<TileObject>().Tile.Corporation;
        List<GameObject> tilesHit = new List<GameObject>();
        Corporation tileHitCorporation;

        // Raycast from the tile and see if it hits an adacent tile up, down, left, right
        tilesHit.Add(RayCastFromTile(placedTile, "up"));
        tilesHit.Add(RayCastFromTile(placedTile, "down"));
        tilesHit.Add(RayCastFromTile(placedTile, "left"));
        tilesHit.Add(RayCastFromTile(placedTile, "right"));
        tilesHit.RemoveAll(item => item == null); // Remove all nulls (non-hit tiles)

        // Loop through list of RayCast2D hit tiles and check for tiles with corporations and without
        if (tilesHit.Count > 0) {
            // TODO: Figure out 3-4 way Merger
            Game.State.Log("Checking for corporation starts and mergers");
            foreach (GameObject tileHit in tilesHit) {
                tileHitCorporation = tileHit.GetComponent<TileObject>().Tile.Corporation;
                if (tileHitCorporation != null)
                    Debug.Log("Tile hit corp: " + tileHitCorporation.Name);
                if (placedTileCorp != null)
                    Debug.Log("Placed tile corp: " + placedTileCorp.Name);

                if (tileHitCorporation != null && placedTileCorp != null) {
                    // If hit tile is part of corporation and we are part of a corporation, then MERGER
                    Debug.Log("If hit tile is part of corporation and we are part of a corporation, then MERGER");

                    if (tileHitCorporation.TileSize > placedTileCorp.TileSize) {
                        // tileHitCorporation survives merger
                        Debug.Log(tileHitCorporation.Name + " survives the merger assuming " + placedTileCorp.Name + " is safe");

                        if (!placedTileCorp.IsSafe)
                            GameManager.Instance.CorporationController.MergeCorporations(tileHitCorporation, placedTileCorp);

                    } else if (tileHitCorporation.TileSize == placedTileCorp.TileSize) {
                        // tileHitCorporation and placedTileCorp are same, players choice on who survives
                        Debug.Log(tileHitCorporation.Name + " and " + placedTileCorp.Name
                            + " are same, players choice on who survives. For now: " + placedTileCorp.Name);

                        if (!tileHitCorporation.IsSafe)
                            // TODO: Players choice, for now placed tile
                            GameManager.Instance.CorporationController.MergeCorporations(placedTileCorp, tileHitCorporation);

                    } else {
                        // placedtileCorp survives merger
                        Game.State.Log(placedTileCorp.Name + " survives merger");

                        if (!tileHitCorporation.IsSafe)
                            GameManager.Instance.CorporationController.MergeCorporations(placedTileCorp, tileHitCorporation);
                    }

                } else if ((tileHitCorporation != null && placedTileCorp == null) || (tileHitCorporation == null && placedTileCorp != null)) {
                    // If hit tile is part of a corporation and the placed tile is not, then add it to existing corporation
                    Game.State.Log(tileHit.name + " is part of Corporation: " + tileHitCorporation.Name
                        + ". Adding " + placedTile.name + " to " + tileHitCorporation.Name);
                    // Set placed tile corp to hit corp
                    placedTileCorp = tileHitCorporation;
                    Game.State.Log(tileHitCorporation.Name + " old size: " + tileHitCorporation.TileSize.ToString());
                    GameManager.Instance.CorporationController.IncreaseSize(tileHitCorporation, 1, placedTile);
                    Game.State.Log(tileHitCorporation.Name + " new size: " + tileHitCorporation.TileSize.ToString());

                } else {
                    // If both tiles are not part of corporation, then found a corporation!
                    Game.State.Log("Both tiles are not part of a corp, so founding new corp!");
                    // TODO: Players Choice, for now random
                    List<Corporation> availableCorporations = GameManager.Instance.CorporationController.AvailableToFound();

                    if (availableCorporations.Count > 0) {
                        Corporation selectedCorporation = GameManager.Instance.CorporationController.RandomCorporation();
                        Game.State.Log("Randomly chose: " + selectedCorporation.Name);
                        GameManager.Instance.TileController.SetTileCorporation(placedTile, selectedCorporation);
                        GameManager.Instance.TileController.SetTileCorporation(tileHit, selectedCorporation);
                    } else {
                        Game.State.Log("No corporations are available. Try Merging some corporations first.");

                    }
                }
            }
        }
    }
}