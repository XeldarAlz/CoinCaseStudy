using UnityEngine;

namespace WheelOfFortuneGame
{
    /// <summary>
    /// Represents the string configurations for the Wheel of Fortune game.
    /// </summary>
    [CreateAssetMenu(menuName = "WheelOfFortune/Strings", fileName = "WheelOfFortuneStrings")]
    public class WheelOfFortuneStrings : ScriptableObject
    {
        [Header("Gameplay Messages")]
       
        [Tooltip("Message displayed to greet the player and introduce the game.")]
        public string greetings = "Spin the wheel to win double amount of your bet!";
        
        [Tooltip("Prompt for the player to select a winning number.")]
        public string chooseNumber = "Choose the winning number!";
        
        [Tooltip("Message displayed when a number is selected. {0} is a placeholder for the selected number.")]
        public string selectedNumber = "You have selected {0}!";

        [Tooltip("Message prompting the player to select a valid number.")]
        public string selectValidNumber = "Please select a number to bet on!";

        [Tooltip("Message prompting the player to enter a valid bet amount.")]
        public string enterValidBet = "Enter a valid bet amount!";

        [Tooltip("Message displayed when the player doesn't have enough coins to place a bet.")]
        public string notEnoughCoins = "Not enough coins!";
        
        [Tooltip("Format string for displaying total coins.")]
        public string totalCoins = "Coins: {0}";
        
        [Tooltip("Format string for displaying won coins.")]
        public string wonCoins = "You won {0} coins!";
        
        [Tooltip("Format string for displaying lost coins.")]
        public string loseCoins = "You lose {0} coins!";

        [Tooltip("Message displayed while the wheel is spinning.")]
        public string spinning = "Spinning!";
    }
}