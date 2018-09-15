// <copyright file="ArguementsHandler.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Bot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BotDot.BusinessLogic.Bot.Models;

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
        /// Get Dictionary of Download Arguments and there values
        /// </summary>
        /// <returns>Dictionary Download Arguments * Argument Value</returns>
        public Download GetDownloadCommandArguements()
        {
            var model = new Download();

            if (this.args.Count <= 2)
            {
                return null;
            }

            for (var i = 2; i < this.args.Count; i++)
            {
                if (this.args[i].StartsWith("--") && Enum.TryParse(this.args[i].Replace("--", string.Empty), true, out Download.CommandArguements parsedCommands))
                {
                    switch (parsedCommands)
                    {
                        case Download.CommandArguements.Start:
                            model.Start = this.args[i + 1];
                            break;
                        case Download.CommandArguements.End:
                            model.End = this.args[i + 1];
                            break;
                    }
                }
            }

            var uriStr = this.args.LastOrDefault();

            if (Uri.TryCreate(uriStr, UriKind.Absolute, out Uri uri))
            {
                model.Uri = uri;
            }

            return model;
        }
    }
}
