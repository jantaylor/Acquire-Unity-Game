using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Class Attributes

    private List<Player> _players = new List<Player>();
    static Color[] Colors = new Color[] { Color.magenta, Color.red, Color.cyan, Color.blue, Color.green, Color.yellow };

    #endregion

    #region Class Functions

    /// <summary>
    /// Return list of all players
    /// </summary>
    /// <returns>List of Players</returns>
    public List<Player> Players() {
        return _players;
    }

    /// <summary>
    /// Return specifc player by passing in their id
    /// </summary>
    /// <param name="id">player id</param>
    /// <returns>Player object</returns>
    public Player Player(int id) {
        return _players.Find(player => player.Id.Equals(id));
    }

    public void CreatePlayers(int numberOfPlayers) {
        for (int i = 0; i < numberOfPlayers; ++i) {
            _players.Add(new Player(i, "Player " + (i + 1).ToString(), GetColorById(i)));
        }
    }

    /// <summary>
    /// Supply an enum int-value and get the string-value
    /// </summary>
    /// <param name="id">int value</param>
    /// <returns>string value</returns>
    public Color GetColorById(int id) {
        return Colors[id];
    }

    public string SetPlayerName(int id, string newName) {
        _players[id].Name = newName;
        return _players[id].Name;
    }

    public void GetPlayerStocks(int id) {
        // TODO: return stocks
    }

    public void GetPlayerTiles(Player player) {
        string strTiles = "";
        foreach (Tile tile in player.Tiles)
            strTiles += tile.Number + tile.Letter + " ";

        Debug.Log(player.Name + " tiles: " + strTiles);
    }

    public void GivePlayerStocks(int id) {
        // TODO: Give player stocks
    }

    public void GivePlayerTile(Player player, Tile tile) {
        player.Tiles.Add(tile);
    }

    public void RemovePlayerTile(Player player, Tile tile) {
        player.Tiles.Remove(tile);
    }

    #endregion

    #region NetworkSpecific

    /// <summary>
    /// For Network Player to set up a new player "locally"
    /// </summary>
    /// <param name="playerName">Player's Name from Lobby</param>
    /// <param name="id">Their ID from lobby</param>
    /// <param name="color">Their chosen color from lobby for game log box</param>
    public void SetupLocalPlayer(string playerName, int id, Color color) {
        _players.Add(new Player(id, playerName, color));
    }

    /// <summary>
    /// For Network Player to Start their turn
    /// </summary>
    public void TurnStart() {
        // TODO: Something?
    }

    /// <summary>
    /// For Network Player to end their turn
    /// </summary>
    public void TurnEnd() {
        GameManager.Instance.Endturn();
    }

    #endregion
}
