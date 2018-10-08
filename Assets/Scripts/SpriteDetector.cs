using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteDetector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    Physics2DRaycaster physicsRaycaster;
    public float distance = 100f;
    private GameObject _boardTile;
    public Player Player { get; set; }
    public Tile Tile { get; set; }

    public void OnPointerClick(PointerEventData eventData) {
        // Only the active player can place a tile once
        if (Player == GameManager.Instance.ActivePlayer && !GameManager.Instance.TilePlaced) {
            Debug.Log("You clicked Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

            GameManager.Instance.BoardController.PlaceTileOnBoard(_boardTile);

            Destroy(this.gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Player == GameManager.Instance.ActivePlayer && !GameManager.Instance.TilePlaced) {
            Debug.Log("You hovered over Tile: " + Tile.Id + " - " + Tile.Number + Tile.Letter);

            _boardTile = GameManager.Instance.TileController.CreateTileObject(Tile, Tile.Position);
            GameManager.Instance.BoardController.HighlightBoard(_boardTile, this.gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (Player == GameManager.Instance.ActivePlayer) {
            Destroy(_boardTile);
        }
    }
}
