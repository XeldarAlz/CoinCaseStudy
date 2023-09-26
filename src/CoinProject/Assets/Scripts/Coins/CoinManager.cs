using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Coins
{
    /// <summary>
    /// Manages the coin currency in the game, allowing players to earn and spend coins.
    /// </summary>
    public class CoinManager : MonoBehaviour, ICoinManager
    {
        /// <summary>
        /// Event triggered when the coin amount changes.
        /// </summary>
        public event Action CoinChanged;

        /// <summary>
        /// Gets the maximum count of free coins that can be claimed.
        /// </summary>
        public int MaxFreeCoinCount => maxFreeCoinCount;

        /// <summary>
        /// Gets the hour of the day when coins can be claimed.
        /// </summary>
        public int ClaimHour => claimHour;

        [Tooltip("The hour of the day when coins can be claimed. UTC+0")]
        [SerializeField]
        [Range(0, 24)]
        private int claimHour = 13;

        [Tooltip("Maximum count of free coins that can be claimed.")]
        [SerializeField]
        [Range(0, 10)]
        private int maxFreeCoinCount = 5;

        private int _currentCoins;

        [Inject] private ICoinPersistence _coinPersistence;

        private void Awake()
        {
            _currentCoins = _coinPersistence.LoadCoins();
        }

        /// <summary>
        /// Gets the current coin amount.
        /// </summary>
        public int CurrentCoins
        {
            get => _currentCoins;
            private set
            {
                if (_currentCoins != value)
                {
                    _currentCoins = value;
                    _coinPersistence.SaveCoins(_currentCoins);
                    CoinChanged?.Invoke();
                }
            }
        }

        /// <summary>
        /// Increases the current coin amount by the specified amount.
        /// </summary>
        /// <param name="amount">Amount of coins to earn.</param>
        public void EarnCoins(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot earn negative coins.");
                return;
            }
            CurrentCoins += amount;
        }

        /// <summary>
        /// Attempts to spend the specified amount of coins.
        /// </summary>
        /// <param name="amount">Amount of coins to spend.</param>
        /// <returns>True if coins were successfully spent, false otherwise.</returns>
        public bool SpendCoins(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot spend negative coins.");
                return false;
            }
            if (CurrentCoins >= amount)
            {
                CurrentCoins -= amount;
                return true;
            }
            Debug.LogWarning("Not enough coins.");
            return false;
        }

        /// <summary>
        /// Saves the current coin amount to persistence when the application is paused.
        /// </summary>
        /// <param name="pauseStatus">Whether the application is paused.</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _coinPersistence.SaveCoins(CurrentCoins);
            }
        }

        /// <summary>
        /// Saves the current coin amount to persistence when the application quits.
        /// </summary>
        private void OnApplicationQuit()
        {
            _coinPersistence.SaveCoins(CurrentCoins);
        }
    }
}