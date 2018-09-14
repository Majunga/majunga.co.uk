namespace BotDot.BusinessLogic.Bot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BotDot.Helpers;

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

        public Tuple<bool, string> MapAndValidate(Dictionary<CommandArguements, string> argsKeyValuePairs)
        {
            if (!argsKeyValuePairs.Any(x => x.Key == CommandArguements.Url))
            {
                return Tuple.Create(false, "Failed No Url specified");
            }

            if(!Uri.TryCreate(argsKeyValuePairs[CommandArguements.Url], UriKind.RelativeOrAbsolute, out Uri uri))
            {
                return Tuple.Create(false, "Failed Invalid Url");
            }

            var startTime = argsKeyValuePairs[CommandArguements.Start];
            var endTime = argsKeyValuePairs[CommandArguements.End];

            if (!string.IsNullOrWhiteSpace(startTime) && Time.Validate(startTime))
            {
                return Tuple.Create(false, "Failed Start time is invalid. All times should be in HH:MM:SS format");
            }

            if (!string.IsNullOrWhiteSpace(endTime) && Time.Validate(endTime))
            {
                return Tuple.Create(false, "Failed End time is invalid. All times should be in HH:MM:SS format");
            }

            this.Uri = uri;
            this.Start = startTime;
            this.End = endTime;

            return Tuple.Create(true, string.Empty);
        }
    }
}
