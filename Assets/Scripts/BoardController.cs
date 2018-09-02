using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public GameObject tile;
    private Board board = new Board(10, 10);
    private Transform boardObject;
    private List<Vector3> gridPositions = new List<Vector3>();

    public void SetupBoard() {
        //Creates the empty tiles
        BoardSetup();
    }

    /// <summary>
    /// Sets the board up with an empty tile
    /// </summary>
    private void BoardSetup() {
        // Instantiate boardObject and set it to the Board's transform
        boardObject = GameObject.Find("Board").transform;

        int id = 0;
        for (int y = board.Columns - 1; y > -1; --y) {
            for (int x = 0; x < board.Rows; ++x) {
                // Instantiate the emptyTile prefab
                GameObject instance = Instantiate(tile);

                // Don't render the tile or text
                instance.GetComponent<SpriteRenderer>().enabled = false;
                GameManager.Instance.tileController.SetTileText(instance, "", "");

                // Set the parent of the new empty Tile to "Board"
                instance.transform.SetParent(boardObject);

                // Set the position to the x and y (with adding some for placeholder graphics not being perfect
                instance.transform.localPosition = new Vector3(x + .6f, y + .5f, 0f);

                // Rename emptyTile to ID
                instance.name = id.ToString();

                // Increase id
                ++id;
            }
        }
    }
}