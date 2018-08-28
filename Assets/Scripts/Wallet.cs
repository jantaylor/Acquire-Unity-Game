using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wallet holds the player's money
/// </summary>
public class Wallet {

    #region Class Attributes

    private Player _player;
    private int _amount;

    #endregion

    public Player Player {
        get { return _player; }
        set { _player = value; }
    }

    public int Amount {
        get { return _amount; }
        set {
            if (value < 0)
                _amount = 0;
            else
                _amount = value;
        }
    }

    public Wallet() {

    }

    public Wallet(Player player, int amount) {
        _player = player;
        _amount = amount;
    }

    ~Wallet() {
        Debug.Log("Wallet was removed");
    }
}
