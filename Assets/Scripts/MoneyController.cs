using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The MoneyController holds each players wallet instead of on each player
/// </summary>
public class MoneyController : MonoBehaviour {

    #region Class Imports

    #endregion

    #region Class Attributes

    private List<Wallet> wallets;

    #endregion

    #region Unity Specific

    #endregion

    #region Class Functions

    /// <summary>
    /// Create wallets for player, call for each new player
    /// </summary>
    /// <param name="players">Players</param>
    public void CreateWallets(List<Player> players) {
        foreach (Player player in players)
            wallets.Add(new Wallet(player, 6000));
    }

    /// <summary>
    /// Get the player's amount of money in their wallet
    /// </summary>
    /// <param name="player">Player</param>
    /// <returns>int amount</returns>
    public int PlayerAmount(Player player) {
        Wallet playerWallet = wallets.Find(wallet => wallet.Player == player);
        return playerWallet.Amount;
    }

    /// <summary>
    /// Increase the amount of money in player's wallet
    /// </summary>
    /// <param name="player">Player</param>
    /// <returns>int amount</returns>
    public int EarnMoney(Player player, int earned) {
        Wallet playerWallet = wallets.Find(wallet => wallet.Player == player);
        playerWallet.Amount += earned;
        return playerWallet.Amount;
    }

    /// <summary>
    /// Reduce the amount of money in player's wallet
    /// </summary>
    /// <param name="player">Player</param>
    /// <returns>int amount</returns>
    public int SpendMoney(Player player, int spent) {
        Wallet playerWallet = wallets.Find(wallet => wallet.Player == player);
        playerWallet.Amount -= spent;
        return playerWallet.Amount;
    }

    #endregion
}
