using System;

namespace Utility
{
    /// <summary>
    /// Provides utility methods related to time and timestamps.
    /// </summary>
    public static class TimerUtility
    {
        private static readonly DateTime UnixEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Gets the current UTC time.
        /// </summary>
        /// <value>The current UTC time.</value>
        public static DateTime CurrentTime => DateTime.UtcNow;

        /// <summary>
        /// Converts a Unix timestamp to a DateTime object.
        /// </summary>
        /// <param name="timestamp">The Unix timestamp to convert.</param>
        /// <returns>The DateTime representation of the provided timestamp.</returns>
        public static DateTime ConvertTimestampToDateTime(double timestamp) => UnixEpochStart.AddSeconds(timestamp);

        /// <summary>
        /// Converts a DateTime object to a Unix timestamp.
        /// </summary>
        /// <param name="dateTime">The DateTime object to convert.</param>
        /// <returns>The Unix timestamp representation of the provided DateTime.</returns>
        public static double ConvertDateTimeToTimestamp(DateTime dateTime) => (dateTime - UnixEpochStart).TotalSeconds;
    }
}
