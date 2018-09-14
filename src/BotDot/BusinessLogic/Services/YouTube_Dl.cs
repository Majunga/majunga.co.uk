// <copyright file="YouTube_Dl.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Services
{
    using BotDot.BusinessLogic.Services.Interfaces;
    using BotDot.Helpers;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Youtube-Dl is used for downloading Youtube videos
    /// </summary>
    public class Youtube_Dl : IYoutubeDownload
    {
        private string outputPath;

        public Youtube_Dl(string outputPath)
        {
            this.outputPath = outputPath;
        }
        /// <summary>
        /// Download Video
        /// </summary>
        /// <param name="uri">Url to video</param>
        /// <param name="outputPath">Where file should be downloaded to</param>
        /// <returns>The FileInfo of downloaded file</returns>
        public async Task<FileInfo> DownloadVideo(Uri uri)
        {
            string path;
            if (string.IsNullOrWhiteSpace(this.outputPath))
            {
                path = Environment.CurrentDirectory;
            }
            else
            {
                path = Path.GetFullPath(this.outputPath);
            }

            var id = this.GetVideoIdFromQueryString(uri);

            await new ProcessHelper().Run("youtube-dl", $"--restrict-filenames -o {path}\\%(id)s.%(ext)s {uri.ToString()}");

            var filename = Directory.GetFiles(path, $"{id}*.*")?.FirstOrDefault() ?? string.Empty;

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
