// <copyright file="Arguements.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Bot
{
    using BotDot.BusinessLogic.Bot.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Get Arguments from string message
    /// </summary>
    public class ArguementsHandler
    {
        private List<string> args;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArguementsHandler"/> class.
        /// </summary>
        /// <param name="argsStr">Argument String</param>
        public ArguementsHandler(string argsStr)
        {
            if (string.IsNullOrWhiteSpace(argsStr))
            {
                this.args = new List<string>();
            }
            else
            {
                this.args = argsStr.Split(' ').ToList();
            }
        }

        /// <summary>
        /// Message is actionable by bot
        /// </summary>
        /// <returns>Bool</returns>
        public bool CanAction()
        {
            return this.args.Count > 0 && this.args[0].ToLowerInvariant() == "bot";
        }

        /// <summary>
        /// Get Command from Arguements
        /// </summary>
        /// <returns>Command (Defaults to help if not found)</returns>
        public CommandHandler.Commands GetCommand()
        {
            foreach (var arg in this.args)
            {
                if (Enum.TryParse(arg, true, out CommandHandler.Commands parsedCommands))
                {
                    return parsedCommands;
                }
            }

            return CommandHandler.Commands.Help;
        }

        /// <summary>
        /// Get Tuple list of Download Arguments and there values
        /// </summary>
        /// <returns>List of Tuple of Download Arguments * Argument Value</returns>
        public List<Tuple<Download.CommandArguements, string>> GetDownloadCommandArguements()
        {
            var arguementsAndValues = new List<Tuple<Download.CommandArguements, string>>();

            if (this.args.Count <= 2)
            {
                return arguementsAndValues;
            }

            for (var i = 2; i < this.args.Count; i++)
            {
                if (this.args[i].StartsWith("--") && Enum.TryParse(this.args[i].Replace("--", string.Empty), true, out Download.CommandArguements parsedCommands))
                {
                    arguementsAndValues.Add(Tuple.Create(parsedCommands, this.args[i + 1]));
                }
            }

            var urlStr = this.args.LastOrDefault();

            if (Uri.IsWellFormedUriString(urlStr, UriKind.RelativeOrAbsolute))
            {
                arguementsAndValues.Add(Tuple.Create(Download.CommandArguements.Url, urlStr));
            }

            return arguementsAndValues;
        }
    }
}
