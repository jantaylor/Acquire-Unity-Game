using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class TileObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    private GameObject _boardTile;

    public Tile Tile { get; set; }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("You clicked Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

        GameManager.Instance.boardController.PlaceTileOnBoard(_boardTile);

        Destroy(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("You hovered over Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

        _boardTile = GameManager.Instance.tileController.CreateTileObject(Tile, Tile.Position, new Vector3(1f, 1f, 0.1f));
        GameManager.Instance.boardController.HighlightBoard(_boardTile, this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Destroy(_boardTile);
    }

}
