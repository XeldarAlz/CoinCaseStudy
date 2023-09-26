using System;
using UnityEngine;
using Utility;

namespace Coins
{
    /// <summary>
    /// Provides an implementation of the ICoinPersistence interface using Unity's PlayerPrefs for storage.
    /// This class handles the loading and saving of coin data to the player's preferences, allowing for simple persistence between game sessions.
    /// </summary>
    public class PlayerPrefsCoinPersistence : ICoinPersistence
    {
        // Constants for PlayerPrefs keys and default values.
        private const string CoinsKey = "PlayerCoins";
        private const string LastClaimTimestampKey = "LastClaimTimestamp";
        private const string ExtraCoinActionCountKey = "ExtraCoinActionCount";
        private const int DefaultStartingCoinsValue = 5;

        /// <summary>
        /// Gets or sets the count of extra coin actions from PlayerPrefs.
        /// </summary>
        public int ExtraCoinActionCount
        {
            get => PlayerPrefs.GetInt(ExtraCoinActionCountKey, 0);
            set => PlayerPrefs.SetInt(ExtraCoinActionCountKey, value);
        }

        /// <summary>
        /// Gets or sets the timestamp of the last coin claim from PlayerPrefs.
        /// </summary>
        public double LastClaimTimestamp
        {
            get => PlayerPrefs.GetFloat(LastClaimTimestampKey, 0);
            set => PlayerPrefs.SetFloat(LastClaimTimestampKey, (float)value);
        }

        /// <summary>
        /// Loads the current amount of coins from PlayerPrefs.
        /// If no coin data is found, the default coin value is returned.
        /// </summary>
        /// <returns>The amount of coins loaded from PlayerPrefs or the default value if not found.</returns>
        public int LoadCoins()
        {
            return !PlayerPrefs.HasKey(CoinsKey) ? DefaultStartingCoinsValue : PlayerPrefs.GetInt(CoinsKey, DefaultStartingCoinsValue);
        }

        /// <summary>
        /// Saves the specified amount of coins to PlayerPrefs.
        /// </summary>
        /// <param name="coins">The amount of coins to be saved.</param>
        public void SaveCoins(int coins) 
        {
            if (coins < 0) {
                Debug.LogError("Invalid coin value to save.");
                return;
            }
            PlayerPrefs.SetInt(CoinsKey, coins);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Initializes the last claim timestamp if it doesn't exist in PlayerPrefs.
        /// </summary>
        public void InitializeLastClaimTimestamp()
        {
            if (!PlayerPrefs.HasKey(LastClaimTimestampKey)) 
            {
                DateTime initialTimestamp = TimerUtility.CurrentTime.Date;
                LastClaimTimestamp = TimerUtility.ConvertDateTimeToTimestamp(initialTimestamp);
            }
        }
    }
}