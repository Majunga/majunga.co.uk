// <copyright file="Time.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace MajungaLibrary.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// time Helpers
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// Validate Time
        /// </summary>
        /// <param name="timeStr">String of Time, Expects HH:MM:SS</param>
        /// <returns>Bool</returns>
        public static bool Validate(string timeStr)
        {
            return Regex.IsMatch(timeStr, @"^([0-1][0-9]|[2][0-3]):([0-5][0-9]):([0-5][0-9])$");
        }
    }
}
