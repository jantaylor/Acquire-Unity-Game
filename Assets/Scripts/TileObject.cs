using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class TileObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    private GameObject _boardTile;

    public Player Player { get; set; }
    public Tile Tile { get; set; }

    public void OnPointerClick(PointerEventData eventData) {
        // Only the active player can go
        if (Player == GameManager.Instance.PlayerController.ActivePlayer) {
            Debug.Log("You clicked Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

            GameManager.Instance.BoardController.PlaceTileOnBoard(_boardTile);

            Destroy(this.gameObject);
            //GameManager.Instance.PlayerController.PlayTile();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Player == GameManager.Instance.PlayerController.ActivePlayer) {
            Debug.Log("You hovered over Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

            _boardTile = GameManager.Instance.TileController.CreateTileObject(Tile, Tile.Position);
            GameManager.Instance.BoardController.HighlightBoard(_boardTile, this.gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (Player == GameManager.Instance.PlayerController.ActivePlayer) {
            Destroy(_boardTile);
        }
    }

}
