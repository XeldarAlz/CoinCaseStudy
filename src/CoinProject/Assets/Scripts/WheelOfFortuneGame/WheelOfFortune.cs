using System.Collections;
using System.Linq;
using Coins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace WheelOfFortuneGame
{
    /// <summary>
    /// Represents a Wheel of Fortune game where players can bet on numbers and spin the wheel.
    /// </summary>
    public class WheelOfFortune : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        [Tooltip("Input field for players to enter their bet amount.")]
        private TMP_InputField betInputField;

        [SerializeField]
        [Tooltip("Transform of the spinning wheel.")]
        private RectTransform wheelTransform;

        [SerializeField]
        [Tooltip("Text field to display game results and messages.")]
        private TextMeshProUGUI resultText;
        
        [SerializeField]
        [Tooltip("Text field to display info about the game.")]
        private TextMeshProUGUI infoText;
        
        [SerializeField]
        [Tooltip("Text field to display total coin text.")]
        private TextMeshProUGUI coinText;

        [Header("Configuration")]
        [SerializeField]
        [Tooltip("String configurations for various game messages.")]
        private WheelOfFortuneStrings strings;

        [SerializeField]
        [Tooltip("Configuration parameters for the game.")]
        private WheelOfFortuneConfig config;

        [Inject] private ICoinManager _coinManager;
        private Button[] _inputButtons;
        private int _spinCount;
        private int _selectedNumber;
        private int _betAmount;

        private void Awake()
        {
            InitializeButtons();
            InitializeTexts();
        }

        /// <summary>
        /// Selects a number for the game.
        /// </summary>
        /// <param name="number">The number to select.</param>
        public void SelectNumber(int number)
        {
            _selectedNumber = number;
            DisplaySelectedNumber();
        }

        /// <summary>
        /// Starts the Wheel of Fortune game.
        /// </summary>
        public void StartGame() 
        {
            if (!IsValidSelection()) {
                DisplayErrorMessage();
                return;
            }
            int winningNumber = DetermineWinningNumber();
            StartCoroutine(SpinWheelTowards(winningNumber));
        }

        private void DisplaySelectedNumber()
        {
            resultText.text = string.Format(strings.selectedNumber, _selectedNumber);
        }

        private bool IsValidSelection()
        {
            return HasSelectedNumber() && IsValidBetAmount() && HasSufficientCoins();
        }

        private bool HasSelectedNumber()
        {
            return _selectedNumber != 0;
        }

        private bool IsValidBetAmount()
        {
            return int.TryParse(betInputField.text, out _betAmount) && _betAmount > 0;
        }

        private bool HasSufficientCoins()
        {
            return _coinManager.CurrentCoins >= _betAmount;
        }

        private void DisplayErrorMessage()
        {
            if (!HasSelectedNumber())
            {
                resultText.text = strings.selectValidNumber;
            }
            else if (_betAmount <= 0)
            {
                resultText.text = strings.enterValidBet;
            }
            else
            {
                resultText.text = strings.notEnoughCoins;
            }
        }

        private int DetermineWinningNumber()
        {
            _spinCount++;
            resultText.text = strings.spinning;

            if (config.winSpinCounts.Contains(_spinCount) ||
                (_spinCount > config.maxSpinCount && Random.value <= config.winProbability))
            {
                return _selectedNumber;
            }

            return GetRandomLosingNumber();
        }

        private int GetRandomLosingNumber()
        {
            int losingNumber;
            do
            {
                losingNumber = Random.Range(config.minNumber, config.maxNumber + 1);
            } while (losingNumber == _selectedNumber);
            return losingNumber;
        }

        private IEnumerator SpinWheelTowards(int winningNumber)
        {
            _coinManager.SpendCoins(_betAmount);
            UpdateCoinText();
            SetGameInteraction(false);
            yield return PerformWheelSpinAnimation(winningNumber);
            DisplaySpinResult(winningNumber);
            SetGameInteraction(true);
        }

        private IEnumerator PerformWheelSpinAnimation(int winningNumber)
        {
            // Initial fast spin
            float elapsed = 0.0f;
            while (elapsed < config.wheelSpinRotationDuration)
            {
                wheelTransform.Rotate(0, 0, 360 * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Smooth rotation to the target angle
            float targetAngle = CalculateTargetAngleForWinningNumber(winningNumber);
            float currentAngle = wheelTransform.eulerAngles.z;
            elapsed = 0.0f;
            while (elapsed < config.wheelSmoothRotationDuration)
            {
                float t = elapsed / config.wheelSmoothRotationDuration;
                float angle = LerpAngle(currentAngle, targetAngle, t);
                wheelTransform.rotation = Quaternion.Euler(0, 0, angle);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Set the final rotation
            wheelTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }

        private void DisplaySpinResult(int winningNumber)
        {
            if (winningNumber == _selectedNumber)
            {
                int wonCoinCount = _betAmount * 2;
                _coinManager.EarnCoins(wonCoinCount);
                resultText.text = string.Format(strings.wonCoins, wonCoinCount);
                
            }
            else
            {
                resultText.text = string.Format(strings.loseCoins, _betAmount);
            }

            UpdateCoinText();
        }

        private void SetGameInteraction(bool value)
        {
            foreach (Button button in _inputButtons)
            {
                button.interactable = value;
            }
        }

        private void InitializeButtons()
        {
            _inputButtons = GetComponentsInChildren<Button>();
        }
        
        private void InitializeTexts()
        {
            infoText.text = strings.greetings;
            resultText.text = strings.chooseNumber;
            UpdateCoinText();
        }

        private void UpdateCoinText()
        {
            coinText.text = string.Format(strings.totalCoins, _coinManager.CurrentCoins);
        }

        private float CalculateTargetAngleForWinningNumber(int winningNumber)
        {
            // The reason of why 22 is a magic number here is because of the custom wheel image, and its slice angles.
            return -22f + (45f * (winningNumber - 1));
        }

        private float LerpAngle(float from, float to, float t)
        {
            float angleDifference = (to - from + 360) % 360;
            return from + angleDifference * t;
        }
    }
}