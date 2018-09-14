namespace BotDot.BusinessLogic.Bot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Download static Methods and Extension
    /// </summary>
    public static class DownloadExtension
    {
        /// <summary>
        /// Get Value of Command Arguments Tuple List
        /// </summary>
        /// <param name="tupleList">Command Arguments * Values List</param>
        /// <param name="command">Commands Value to extract</param>
        /// <returns>Value</returns>
        public static string GetValue(this List<Tuple<Download.CommandArguements, string>> tupleList, Download.CommandArguements command)
        {
            return tupleList.FirstOrDefault(x => x.Item1 == command)?.Item2 ?? string.Empty;
        }
    }

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

        public Tuple<bool, string> Validate(Dictionary<CommandArguements, string> argsKeyValuePairs)
        {
            return null;
        }
    }
}
