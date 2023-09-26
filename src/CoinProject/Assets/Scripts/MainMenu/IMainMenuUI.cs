using System;

namespace MainMenu
{
    /// <summary>
    /// Represents the main menu user interface functionalities.
    /// This interface provides methods to display and update various UI elements in the main menu.
    /// </summary>
    public interface IMainMenuUI
    {
        /// <summary>
        /// Displays the status of the extra coin availability and the time until the next availability.
        /// </summary>
        /// <param name="isAvailable">Indicates whether the extra coin is currently available.</param>
        /// <param name="timeUntilNextAvailability">The time remaining until the next extra coin becomes available.</param>
        void DisplayExtraCoinStatus(bool isAvailable, TimeSpan timeUntilNextAvailability);

        /// <summary>
        /// Displays the status of the daily bonus claim availability and the time until the next availability.
        /// </summary>
        /// <param name="isAvailable">Indicates whether the daily bonus can currently be claimed.</param>
        /// <param name="timeUntilNextAvailability">The time remaining until the next daily bonus becomes available.</param>
        void DisplayClaimBonusStatus(bool isAvailable, TimeSpan timeUntilNextAvailability);

        /// <summary>
        /// Displays a blocking popup on the main menu.
        /// </summary>
        void ShowBlockingPopup();

        /// <summary>
        /// Displays the Wheel of Fortune popup on the main menu.
        /// </summary>
        void ShowWheelOfFortunePopup();
    }
}
