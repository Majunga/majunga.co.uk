// <copyright file="YouTube_Dl.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using BotDot.BusinessLogic.Services.Interfaces;
    using BotDot.Helpers;

    /// <summary>
    /// Youtube-Dl is used for downloading Youtube videos
    /// </summary>
    public class Youtube_Dl : IYoutubeDownload
    {
        private string outputPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Youtube_Dl"/> class.
        /// </summary>
        /// <param name="outputPath">Output path of downloaded Video</param>
        public Youtube_Dl(string outputPath)
        {
            this.outputPath = outputPath;
        }

        /// <inheritdoc/>
        public async Task<FileInfo> DownloadVideo(Uri uri, string userId)
        {
            var path = FileHelper.GetFullPath(this.outputPath);

            var id = this.GetVideoIdFromQueryString(uri);

            Console.WriteLine(Environment.CurrentDirectory);

            var arguments = $"--restrict-filenames -o \"{path}/{userId}%(id)sOriginal.%(ext)s\" {uri.ToString()}";

            Console.WriteLine(arguments);

            await new ProcessHelper().Run("youtube-dl", arguments);

            var filename = Directory.GetFiles(path, $"{userId}{id}*.*")?.FirstOrDefault() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(nameof(filename), "Downloaded file couldn't be found");
            }

            return new FileInfo(filename);
        }

        /// <summary>
        /// Get the Video Id from the Uri's Query string
        /// </summary>
        /// <param name="uri">Uri of website</param>
        /// <returns>Id of video</returns>
        public string GetVideoIdFromQueryString(Uri uri)
        {
            var queryVideoId = string.Concat(uri.Query.TakeWhile(x => x != '&').Select(c => c)) ?? string.Empty;

            if (!queryVideoId.StartsWith("?v="))
            {
                throw new ArgumentNullException(nameof(queryVideoId), "Missing Video ID from Uri");
            }

            return queryVideoId.Replace("?v=", string.Empty);
        }
    }
}
