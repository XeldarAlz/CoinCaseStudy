using UnityEngine;

namespace MainMenu
{
    /// <summary>
    /// Represents the string configurations for the Main Menu of the game.
    /// </summary>
    [CreateAssetMenu(menuName = "MainMenu/Strings", fileName = "MainMenuStrings")]
    public class MainMenuStrings : ScriptableObject
    {
        [Header("Coin Messages")]

        [Tooltip("Label for the button or option to claim a free coin.")]
        public string claimFreeCoin = "Claim Free Coin";

        [Tooltip("Format to display the current coin count. {0} is a placeholder for the coin count.")]
        public string coinDisplayFormat = "Coins: {0}";

        [Tooltip("Format to display the option to get extra coins. {0} and {1} are placeholders for the current count and the maximum count, respectively.")]
        public string getExtraCoinFormat = "Get Extra Coin {0}/{1}";

        [Header("Timer Messages")]

        [Tooltip("Format to display the time remaining until an extra coin is available. {0:hh\\:mm\\:ss} is a placeholder for the time in hours, minutes, and seconds.")]
        public string extraCoinAvailableInFormat = "Extra Coin Available in: {0:hh\\:mm\\:ss}";

        [Tooltip("Format to display the time remaining until the daily bonus is available. {0:hh\\:mm\\:ss} is a placeholder for the time in hours, minutes, and seconds.")]
        public string dailyBonusInFormat = "Daily bonus in: {0:hh\\:mm\\:ss}";
    }
}