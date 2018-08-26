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

    public List<Player> Players() {
        return _players;
    }

    public void CreatePlayers(int numberOfPlayers = 3) {
        for (int i = 0; i < numberOfPlayers; ++i)
            _players.Add(new Player(i, "Player " + i+1, GetColorById(i)));
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

    public void GetPlayerTiles(int id) {
        // TODO: return tiles
    }

    public void GivePlayerStocks(int id) {
        // TODO: Give player stocks
    }

    public void GivePlayerTiles(int id) {
        // TODO: Give player a tile
    }

    #endregion
}
