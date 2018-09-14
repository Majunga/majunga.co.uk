// <copyright file="CommandHandler.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Bot
{
    using BotDot.BusinessLogic.Bot.Models;
    using BotDot.BusinessLogic.Services.Interfaces;
    using BotDot.Helpers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Command Handler
    /// </summary>
    public class CommandHandler
    {
        private readonly IDownloadFile download;
        private readonly IVideoConverter videoConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="download">Downloads files from Internet</param>
        /// <param name="videoConverter">Video conversion</param>
        public CommandHandler(IDownloadFile download, IVideoConverter videoConverter)
        {
            this.download = download;
            this.videoConverter = videoConverter;
        }

        /// <summary>
        /// Commands available
        /// </summary>
        public enum Commands
        {
            /// <summary>
            /// Download Youtube video
            /// </summary>
            Download,

            /// <summary>
            /// Help section
            /// </summary>
            Help
        }

        public async Task DownloadCommand(ArguementsHandler args, BotResponses responses)
        {
            // Get arguments
            var argumentAndValueList = args.GetDownloadCommandArguements();

            if (!argumentAndValueList.Any(x => x.Item1 == Download.CommandArguements.Url))
            {
                await responses.SendMessage("Failed No Url specified");
                return;
            }

            var startTime = argumentAndValueList.GetValue(Download.CommandArguements.Start);
            var endTime = argumentAndValueList.GetValue(Download.CommandArguements.End);

            if (!string.IsNullOrWhiteSpace(startTime) && Time.Validate(startTime) )
            {
                await responses.SendMessage("Failed Start time is invalid. All times should be in HH:MM:SS format");
                return;
            }

            if (!string.IsNullOrWhiteSpace(endTime) && Time.Validate(endTime))
            {
                await responses.SendMessage("Failed End time is invalid. All times should be in HH:MM:SS format");
                return;
            }

            // Download file
            if (!Uri.TryCreate(argumentAndValueList.GetValue(Download.CommandArguements.Url), UriKind.RelativeOrAbsolute, out Uri uri))
            {
                await responses.SendMessage("Failed invalid Url specified");
                return;
            }

            var file = this.download.YouTubeVideo(uri);

            if (file == null)
            {
                await responses.SendMessage("Failed to download Video.");
                return;
            }

            // Convert to mp4 and if needed trim file
            var formattedVideo = this.videoConverter.ConvertToMp4(file, Tuple.Create(startTime, endTime));

            // Clean up
        }
    }
}
