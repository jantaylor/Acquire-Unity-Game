using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Class Imports

    #endregion

    #region Class Attributes

    private List<Player> _players = new List<Player>();

    enum Colors { red, green, blue, cyan, magenta, yellow };

    #endregion

    #region Unity Specific

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

    public void CreatePlayers(int numberOfPlayers = 3) {
        for (int i = 0; i < numberOfPlayers; ++i)
            _players.Add(new Player(i, "Player " + (i+1).ToString(), GetColorById(i)));

        _players[0].Name = "Cleo"; // Remove - this is for testing
    }

    /// <summary>
    /// Supply an enum int-value and get the string-value
    /// </summary>
    /// <param name="id">int value</param>
    /// <returns>string value</returns>
    public string GetColorById(int id) {
        Colors color = (Colors)id;
        return color.ToString();
    }

    public string GetPlayerName(int id) {
        return _players[id].Name;
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

        Debug.Log("Player tiles: " + strTiles);
    }

    public void GivePlayerStocks(int id) {
        // TODO: Give player stocks
    }

    public void GivePlayerTile(Player player, Tile newTile) {
        player.Tiles.Add(newTile);
    }

    #endregion
}
