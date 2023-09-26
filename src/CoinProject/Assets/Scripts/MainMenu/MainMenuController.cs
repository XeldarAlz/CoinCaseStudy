using System;
using System.Collections;
using Coins;
using UnityEngine;
using Utility;
using Zenject;

namespace MainMenu
{
    /// <summary>
    /// Represents the main menu of the game
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        [Inject] private ICoinManager _coinManager;
        [Inject] private ICoinPersistence _coinPersistence;
        [Inject] private IMainMenuUI _uiManager;

        private int _claimHour;
        private int _maxFreeCoinCount;

        private void Awake()
        {
            _claimHour = _coinManager.ClaimHour;
            _maxFreeCoinCount = _coinManager.MaxFreeCoinCount;
        }

        private void Start()
        {
            _coinPersistence.InitializeLastClaimTimestamp();
            _uiManager.DisplayClaimBonusStatus(CanClaimBonus(), TimeUntilNextClaim());
            _uiManager.DisplayExtraCoinStatus(CanExecuteExtraCoinAction(), TimeUntilNextFreeCoin());
            CheckCountdowns();
        }

        private void CheckCountdowns()
        {
            if (!CanClaimBonus())
            {
                StartCoroutine(ClaimBonusCountdownCoroutine());
            }

            if (!CanExecuteExtraCoinAction())
            {
                StartCoroutine(ExtraCoinCountdownCoroutine());
            }
        }

        private bool CanClaimBonus()
        {
            DateTime lastClaimDate = TimerUtility.ConvertTimestampToDateTime(_coinPersistence.LastClaimTimestamp);
            DateTime nextClaimTime = CalculateNextClaimTime(lastClaimDate);

            return IsTimeForNextClaim(nextClaimTime);
        }
        
        private DateTime CalculateNextClaimTime(DateTime lastClaimDate)
        {
            DateTime sameDayClaimTime = lastClaimDate.Date.AddHours(_claimHour);
    
            if (lastClaimDate < sameDayClaimTime)
            {
                return sameDayClaimTime;
            }
            return lastClaimDate.Date.AddDays(1).AddHours(_claimHour);
        }

        private bool IsTimeForNextClaim(DateTime nextClaimTime)
        {
            if (TimerUtility.CurrentTime >= nextClaimTime)
            {
                NextClaimTimeTriggered();
                return true;
            }

            return false;
        }

        private void NextClaimTimeTriggered()
        {
            _coinPersistence.ExtraCoinActionCount = 0;
        }

        private bool CanExecuteExtraCoinAction()
        {
            return IsWithinMaxFreeCoinLimit();
        }

        private bool IsWithinMaxFreeCoinLimit()
        {
            return _coinPersistence.ExtraCoinActionCount < _maxFreeCoinCount;
        }
        
        /// <summary>
        /// Spends one coin from the player's balance.
        /// </summary>
        public void Action_SpendOneCoin()
        {
            if (_coinManager.SpendCoins(1))
            {
                Debug.Log("Successfully spent 1 coin.");
            }
        }

        /// <summary>
        /// Executes the action to get an extra coin.
        /// </summary>
        public void Action_GetExtraCoin() 
        {
            _coinPersistence.ExtraCoinActionCount++;
            _uiManager.ShowBlockingPopup();
            StartCoroutine(ExtraCoinCountdownCoroutine());
            _coinManager.EarnCoins(1);
            _uiManager.DisplayExtraCoinStatus(CanExecuteExtraCoinAction(), TimeUntilNextFreeCoin());
        }

        /// <summary>
        /// Claims a free coin for the player.
        /// </summary>
        public void Action_ClaimFreeCoin()
        {
            _coinPersistence.LastClaimTimestamp = TimerUtility.ConvertDateTimeToTimestamp(TimerUtility.CurrentTime);
            _coinManager.EarnCoins(1);
            StartCoroutine(ClaimBonusCountdownCoroutine());
        }

        /// <summary>
        /// Opens the Wheel of Fortune popup.
        /// </summary>
        public void Action_WheelOfFortune()
        {
            _uiManager.ShowWheelOfFortunePopup();
        }

        private TimeSpan TimeUntilNextFreeCoin()
        {
            DateTime nextAvailableTime = TimerUtility.CurrentTime.Date.AddDays(1).AddHours(_claimHour);
            return nextAvailableTime - TimerUtility.CurrentTime;
        }

        private IEnumerator ClaimBonusCountdownCoroutine()
        {
            while (true)
            {
                bool canClaimBonus = CanClaimBonus();
                _uiManager.DisplayClaimBonusStatus(canClaimBonus, TimeUntilNextClaim());

                if (canClaimBonus)
                {
                    yield break;
                }

                yield return new WaitForSeconds(1);
            }
        }
        
        private IEnumerator ExtraCoinCountdownCoroutine()
        {
            while (true)
            {
                bool canExecuteExtraCoin = CanExecuteExtraCoinAction();
                _uiManager.DisplayExtraCoinStatus(canExecuteExtraCoin, TimeUntilNextFreeCoin());

                if (canExecuteExtraCoin)
                {
                    yield break;
                }

                yield return new WaitForSeconds(1);
            }
        }

        private TimeSpan TimeUntilNextClaim()
        {
            DateTime lastClaimDate = TimerUtility.ConvertTimestampToDateTime(_coinPersistence.LastClaimTimestamp);
            DateTime nextClaimTime = lastClaimDate < lastClaimDate.Date.AddHours(_claimHour)
                ? lastClaimDate.Date.AddHours(_claimHour)
                : lastClaimDate.Date.AddDays(1).AddHours(_claimHour);

            return nextClaimTime - TimerUtility.CurrentTime;
        }
    }
}
