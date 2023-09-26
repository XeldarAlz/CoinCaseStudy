using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortuneGame;
using BlockingPage;
using Coins;
using TMPro;
using Zenject;

namespace MainMenu
{
    /// <summary>
    /// Manages the main menu's user interface, updating coin counts, bonus statuses, and other UI elements.
    /// </summary>
    public class MainMenuUIManager : MonoBehaviour, IMainMenuUI
    {
        [Header("UI References")]

        [Tooltip("Reference to the main menu strings for UI text.")]
        [SerializeField] private MainMenuStrings mainMenuStrings;

        [Tooltip("Button for getting an extra coin.")]
        [SerializeField] private Button getExtraCoinButton;

        [Tooltip("Button for claiming a free coin.")]
        [SerializeField] private Button claimButton;

        [Tooltip("Text displaying the status of the extra coin.")]
        [SerializeField] private TextMeshProUGUI getExtraCoinText;

        [Tooltip("Text displaying the status of the free coin claim.")]
        [SerializeField] private TextMeshProUGUI claimFreeCoinText;

        [Tooltip("Text displaying the current coin count.")]
        [SerializeField] private TextMeshProUGUI coinText;

        [Tooltip("Popup for blocking wait with a progress bar.")]
        [SerializeField] private BlockingWait blockingWaitPopup;

        [Tooltip("Popup for the Wheel of Fortune game.")]
        [SerializeField] private WheelOfFortune wheelOfFortunePopup;

        [Inject] private ICoinPersistence _coinPersistence;
        [Inject] private ICoinManager _coinManager;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private void OnEnable()
        {
            _coinManager.CoinChanged += CoinChanged;
        }

        private void Start()
        {
            SetCoinCountDisplay(_coinManager.CurrentCoins);
        }

        /// <summary>
        /// Updates the extra coin status on the UI.
        /// </summary>
        /// <param name="isAvailable">Indicates if the extra coin is available.</param>
        /// <param name="timeUntilNextAvailability">Time until the next extra coin availability.</param>
        public void DisplayExtraCoinStatus(bool isAvailable, TimeSpan timeUntilNextAvailability)
        {
            if (isAvailable)
            {
                getExtraCoinButton.interactable = true;
                getExtraCoinText.text = FormatString(mainMenuStrings.getExtraCoinFormat, _coinManager.MaxFreeCoinCount - _coinPersistence.ExtraCoinActionCount, _coinManager.MaxFreeCoinCount);
            }
            else
            {           
                getExtraCoinButton.interactable = false;
                getExtraCoinText.text = FormatString(mainMenuStrings.extraCoinAvailableInFormat, timeUntilNextAvailability);
            }
        }

        /// <summary>
        /// Updates the claim bonus status on the UI.
        /// </summary>
        /// <param name="isAvailable">Indicates if the bonus can be claimed.</param>
        /// <param name="timeUntilNextAvailability">Time until the next bonus claim availability.</param>
        public void DisplayClaimBonusStatus(bool isAvailable, TimeSpan timeUntilNextAvailability)
        {
            _stringBuilder.Clear();
            if (isAvailable)
            {
                claimButton.interactable = true;
                _stringBuilder.Append(mainMenuStrings.claimFreeCoin);
            }
            else
            {
                claimButton.interactable = false;
                _stringBuilder.AppendFormat(mainMenuStrings.dailyBonusInFormat, timeUntilNextAvailability);
            }
            claimFreeCoinText.text = _stringBuilder.ToString();
        }

        /// <summary>
        /// Sets the coin count display on the UI.
        /// </summary>
        /// <param name="coinCount">The current coin count.</param>
        public void SetCoinCountDisplay(int coinCount)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat(mainMenuStrings.coinDisplayFormat, coinCount);
            coinText.text = _stringBuilder.ToString();
        }

        private void CoinChanged()
        {
            SetCoinCountDisplay(_coinManager.CurrentCoins);
        }

        /// <summary>
        /// Shows the blocking popup with a progress bar.
        /// </summary>
        public void ShowBlockingPopup()
        {
            blockingWaitPopup.gameObject.SetActive(true);
            blockingWaitPopup.StartProgressBar();
        }

        /// <summary>
        /// Shows the Wheel of Fortune popup.
        /// </summary>
        public void ShowWheelOfFortunePopup()
        {
            wheelOfFortunePopup.gameObject.SetActive(true);
        }

        private string FormatString(string format, params object[] args)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat(format, args);
            return _stringBuilder.ToString();
        }


        private void OnDisable()
        {
            _coinManager.CoinChanged -= CoinChanged;
        }
    }
}