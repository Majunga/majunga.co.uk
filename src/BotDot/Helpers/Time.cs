namespace BotDot.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// time Helpers
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Validate Time 
        /// </summary>
        /// <param name="timeStr">String of Time, Expects HH:MM:SS</param>
        /// <returns>Bool</returns>
        public bool Validate(string timeStr)
        {
            return Regex.IsMatch(timeStr, @"^([0-5][0-9]):([0-5][0-9]):([0-5][0-9])$");
        }
    }
}
