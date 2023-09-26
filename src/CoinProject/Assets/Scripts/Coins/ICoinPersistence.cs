using System;

namespace Coins
{
    /// <summary>
    /// Defines the operations related to the persistence of coin data.
    /// This interface provides a contract for loading and saving coin amounts, allowing for flexibility in storage mechanisms.
    /// </summary>
    public interface ICoinPersistence
    {
        /// <summary>
        /// Count of extra coin actions from the persistence source.
        /// </summary>
        int ExtraCoinActionCount { get; set; }

        /// <summary>
        /// Timestamp of the last coin claim from the persistence source.
        /// </summary>
        double LastClaimTimestamp { get; set; }

        /// <summary>
        /// Loads the current amount of coins.
        /// </summary>
        /// <returns>The amount of coins loaded.</returns>
        int LoadCoins();

        /// <summary>
        /// Saves the specified amount of coins.
        /// </summary>
        /// <param name="coins">The amount of coins to be saved.</param>
        void SaveCoins(int coins);

        /// <summary>
        /// Initializes the last claim timestamp if it doesn't exist in the persistence source.
        /// </summary>
        void InitializeLastClaimTimestamp();
    }
}