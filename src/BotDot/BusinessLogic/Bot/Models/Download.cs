namespace BotDot.BusinessLogic.Bot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Download Model and Extention
    /// </summary>
    public static class Download
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
        /// Get Value of Command Arguments Tuple List
        /// </summary>
        /// <param name="tupleList">Command Arguments * Values List</param>
        /// <param name="command">Commands Value to extract</param>
        /// <returns>Value</returns>
        public static string GetValue(this List<Tuple<CommandArguements, string>> tupleList, CommandArguements command)
        {
            return tupleList.FirstOrDefault(x => x.Item1 == command)?.Item2 ?? string.Empty;
        }
    }
}
