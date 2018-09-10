using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class TileObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    private Color _yellow = new Color(1, 1, 0.4f, 0.9f);
    private GameObject _boardTile;

    public Tile Tile { get; set; }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("You clicked Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

        GameManager.Instance.boardController.PlaceTileOnBoard(_boardTile);

        Destroy(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("You hovered over Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

        _boardTile = Instantiate(GameManager.Instance.tileController.tilePrefab);
        GameManager.Instance.boardController.HighlightBoard(_boardTile, this.gameObject);

        _boardTile.GetComponent<SpriteRenderer>().color = _yellow;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Destroy(_boardTile);
    }

}
