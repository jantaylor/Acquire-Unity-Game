using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public GameObject emptyTile;
    public GameObject placedTile;
    private Board board = new Board(10, 10);
    private Transform boardObject;
    private List<Vector3> gridPositions = new List<Vector3>();

    public void SetupBoard() {
        //Creates the empty tiles
        BoardSetup();

        //Reset our list of gridpositions.
        InitialiseTileList();
    }

    private void BoardSetup() {
        // Instantiate boardObject and set it to the Board's transform
        boardObject = GameObject.Find("Board").transform;

        Debug.Log(board.Columns);
        // Loop along x axis
        for (int x = 0; x < board.Columns; ++x) {
            // Loop along y axis
            for (int y = 0; y < board.Rows; y++) {
                // Instantiate the emptyTile prefab
                GameObject instance = Instantiate(emptyTile);

                // Set the parent of the new empty Tile to "Board"
                instance.transform.SetParent(boardObject);

                // Set the position to the x and y (with adding some for placeholder graphics not being perfect
                instance.transform.localPosition = new Vector3(x + .6f, y + .5f, 0f);

                // Rename emptyTile to ID
                instance.name = y.ToString() + x.ToString();
            }
        }
    }

    private void InitialiseTileList() {
        //Clear our list gridPositions.
        gridPositions.Clear();

        for (int x = 1; x < board.Columns - 1; x++) {
            for (int y = 1; y < board.Columns - 1; y++) {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
}