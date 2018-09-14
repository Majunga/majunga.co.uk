// <copyright file="IDownload.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Services.Interfaces
{
    using System;
    using System.IO;

    /// <summary>
    /// Downloads files from the internet
    /// </summary>
    public interface IDownloadFile
    {
        /// <summary>
        /// Download YouTube video
        /// </summary>
        /// <param name="uri">Uri to the video</param>
        /// <returns>Downloaded Video file</returns>
        FileInfo YouTubeVideo(Uri uri);
    }
}
