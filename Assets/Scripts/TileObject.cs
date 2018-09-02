using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {

    private Vector3 _hudScale = new Vector3(1.5f, 1.5f, 1f);
    private Vector3 _boardScale = new Vector3(1f, 1f, 1f);
    public Tile Tile { get; set; }

    public void OnMouseDown() {
        Debug.Log("You clicked Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

        this.gameObject.transform.localScale = _boardScale;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.boardController.PlaceTileOnBoard(this.gameObject);
    }

    public void OnMouseEnter() {
        Debug.Log("You hovered over Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);
    }
}
