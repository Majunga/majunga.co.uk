// <copyright file="Download.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace Backend.BusinessLogic.Bot.Models
{
    using MajungaLibrary.Helpers;
    using System;
    using System.Globalization;

    /// <summary>
    /// Download Model and methods
    /// </summary>
    public class Download
    {
        /// <summary>
        /// Download Command Arguements
        /// </summary>
        public enum CommandArguements
        {
            /// <summary>
            /// Url of file to download, last arguement
            /// </summary>
            Url,

            /// <summary>
            /// Time to start video from
            /// </summary>
            Start,

            /// <summary>
            /// Time to end video from
            /// </summary>
            End
        }

        /// <summary>
        /// Gets or sets url to use
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets start Time
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets end Time
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Map values to model and validate
        /// </summary>
        /// <returns>Success * Message</returns>
        public Tuple<bool, string> Validate()
        {
            if (this.Uri == null)
            {
                return Tuple.Create(false, "Failed Url not specified or invalid");
            }

            if (!string.IsNullOrWhiteSpace(this.Start) && !Time.Validate(this.Start))
            {
                return Tuple.Create(false, "Failed Start time is invalid. All times should be in HH:MM:SS format");
            }

            if (!string.IsNullOrWhiteSpace(this.End) && !Time.Validate(this.End))
            {
                return Tuple.Create(false, "Failed End time is invalid. All times should be in HH:MM:SS format");
            }

            if (!string.IsNullOrWhiteSpace(this.Start)
                && !string.IsNullOrWhiteSpace(this.End)
                && DateTime.TryParseExact(this.Start, "HH:mm:ss", CultureInfo.InvariantCulture, style: DateTimeStyles.None, result: out DateTime startDateTime)
                && DateTime.TryParseExact(this.End, "HH:mm:ss", CultureInfo.InvariantCulture, style: DateTimeStyles.None, result: out DateTime endDateTime))
            {
                if (startDateTime.TimeOfDay > endDateTime.TimeOfDay)
                {
                    return Tuple.Create(false, "Failed End time cannot be greater than Start time");
                }
            }

            return Tuple.Create(true, string.Empty);
        }
    }
}
