// <copyright file="IVideoConverter.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Services.Interfaces
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Converts video formats
    /// </summary>
    public interface IVideoConverter
    {
        /// <summary>
        /// Convert any video type to MP4
        /// </summary>
        /// <param name="file">File to convert</param>
        /// <param name="times">Cut file to size</param>
        /// <returns>Converted video File</returns>
        Task<FileInfo> ConvertToMp4(FileInfo file, Tuple<string, string> times);
    }
}
