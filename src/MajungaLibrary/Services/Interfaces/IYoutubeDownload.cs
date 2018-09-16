// <copyright file="IYoutubeDownload.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace MajungaLibrary.BusinessLogic.Services.Interfaces
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Downloads files from the internet
    /// </summary>
    public interface IYoutubeDownload
    {
        /// <summary>
        /// Download YouTube video
        /// </summary>
        /// <param name="uri">Uri to the video</param>
        /// <param name="userId">UserId of requester</param>
        /// <returns>Task of Downloaded Video file</returns>
        Task<FileInfo> DownloadVideo(Uri uri, string userId);
    }
}
