// <copyright file="CommandHandler.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Bot
{
    using System;
    using System.Threading.Tasks;
    using BotDot.BusinessLogic.Services.Interfaces;

    /// <summary>
    /// Command Handler
    /// </summary>
    public class CommandHandler
    {
        private readonly IYoutubeDownload download;
        private readonly IVideoConverter videoConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="download">Downloads files from Internet</param>
        /// <param name="videoConverter">Video conversion</param>
        public CommandHandler(IYoutubeDownload download, IVideoConverter videoConverter)
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

        /// <summary>
        /// Run actions for Download command
        /// </summary>
        /// <param name="args">ArgumentsHandler</param>
        /// <param name="responses">BotResponseHandler</param>
        /// <returns>Task</returns>
        public async Task DownloadCommand(ArguementsHandler args, BotResponseHandler responses)
        {
            // Get arguments
            var model = args.GetDownloadCommandArguements();
            var result = model.Validate();

            if (!result.Item1)
            {
                await responses.SendMessage(result.Item2);
                return;
            }

            // Download file
            await responses.SendMessage("Downloading video");

            var file = await this.download.DownloadVideo(model.Uri);

            if (!file.Exists)
            {
                await responses.SendMessage("Failed to download Video.");
                return;
            }

            // Convert to mp4 and if needed trim file
            await responses.SendMessage("Formating video");

            var formattedVideo = await this.videoConverter.ConvertToMp4(file, Tuple.Create(model.Start, model.End));

            if (!formattedVideo.Exists)
            {
                await responses.SendMessage("Failed to format Video.");
                return;
            }

            await responses.SendMessage($"Done! {Environment.GetEnvironmentVariable("URL")}/static/{formattedVideo.Name}");

            // Clean up
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
        }
    }
}
