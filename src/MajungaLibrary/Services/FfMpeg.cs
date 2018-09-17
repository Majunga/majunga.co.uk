// <copyright file="FfMpeg.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace MajungaLibrary.BusinessLogic.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using MajungaLibrary.BusinessLogic.Services.Interfaces;
    using MajungaLibrary.Helpers;

    /// <summary>
    /// FFMpeg Video Converter
    /// </summary>
    public class FFMpeg : IVideoConverter
    {
        private readonly string outputPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FFMpeg"/> class.
        /// </summary>
        /// <param name="outputPath">Output path of converted file</param>
        public FFMpeg(string outputPath)
        {
            this.outputPath = outputPath;
        }

        /// <summary>
        /// Convert file to Mp4
        /// </summary>
        /// <param name="file">File to convert</param>
        /// <param name="times">Times tp cut file to</param>
        /// <returns>async task of Converted Files Info </returns>
        public async Task<FileInfo> ConvertToMp4(FileInfo file, Tuple<string, string> times)
        {
            var path = FileHelper.GetFullPath(this.outputPath);
            var newFilename = $"{path}/{file.Name.Replace("Original", string.Empty).Replace(file.Extension, string.Empty)}.mp4";
            var arguments = $"-y -i {file.FullName} ";

            if (!string.IsNullOrWhiteSpace(times?.Item1))
            {
                arguments += $" -ss {times.Item1}";
            }

            if (!string.IsNullOrWhiteSpace(times?.Item2))
            {
                arguments += $" -to {times.Item2}";
            }

            arguments += $" {newFilename}";

            Console.WriteLine(arguments);

            await new ProcessHelper().Run("ffmpeg", arguments);

            return new FileInfo(newFilename);
        }
    }
}
