using System;

namespace Coins
{
    /// <summary>
    /// Defines the operations and properties related to coin management in the game.
    /// This interface provides a contract for managing, earning, and spending coins.
    /// </summary>
    public interface ICoinManager
    {
        /// <summary>
        /// Current amount of coins available to the player.
        /// </summary>
        int CurrentCoins { get; }

        /// <summary>
        /// Maximum count of free coins that can be claimed.
        /// </summary>
        int MaxFreeCoinCount { get; }

        /// <summary>
        /// Hour of the day when coins can be claimed.
        /// </summary>
        int ClaimHour { get; }

        /// <summary>
        /// Event triggered when the coin amount changes.
        /// </summary>
        event Action CoinChanged;

        /// <summary>
        /// Increases the current coin amount by the specified amount, simulating the earning of coins.
        /// </summary>
        /// <param name="amount">Amount of coins to be added to the current coin balance.</param>
        void EarnCoins(int amount);

        /// <summary>
        /// Attempts to spend the specified amount of coins from the player's balance.
        /// </summary>
        /// <param name="amount">Amount of coins to be spent.</param>
        /// <returns>True if the coins were successfully spent (i.e., player had enough coins), false otherwise.</returns>
        bool SpendCoins(int amount);
    }
}
