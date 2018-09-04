using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {

    private Vector3 _hudScale = new Vector3(1.5f, 1.5f, 1f);
    private Vector3 _boardScale = new Vector3(1f, 1f, 1f);
    private Color _yellow = new Color(1, 1, 0.4f, 0.9f);
    private GameObject _highlight;

    public Tile Tile { get; set; }

    public void OnMouseDown() {
        Debug.Log("You clicked Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

        DestroyHighlight();
        this.gameObject.transform.localScale = _boardScale;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.boardController.PlaceTileOnBoard(this.gameObject);
    }

    public void OnMouseEnter() {
        Debug.Log("You hovered over Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

        _highlight = Instantiate(this.gameObject);
        GameManager.Instance.boardController.HighlightBoard(_highlight, this.gameObject);
        _highlight.GetComponent<BoxCollider2D>().enabled = false;
        _highlight.transform.localScale = _boardScale;
        _highlight.GetComponent<SpriteRenderer>().color = _yellow;
    }

    public void OnMouseExit() {
        DestroyHighlight();
    }

    private void DestroyHighlight() {
        Destroy(_highlight);
    }

}
