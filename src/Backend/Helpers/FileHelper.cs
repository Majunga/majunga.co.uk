// <copyright file="FileHelper.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.Helpers
{
    using System;
    using System.IO;

    /// <summary>
    /// Helpers for file related taskss
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Delete files in Directory that are older than 15 mins
        /// </summary>
        /// <param name="path">Directory Path</param>
        public static void DeleteExpiredFile(string path)
        {
            Console.WriteLine("Deleting Expired files");
            foreach (var file in Directory.GetFiles(path))
            {
                var fileInfo = new FileInfo(file);
                Console.WriteLine($"Trying to delete {fileInfo.Name}");
                if (fileInfo.CreationTime < DateTime.Now.AddMinutes(-30) && fileInfo.Name.Contains("test.txt"))
                {
                    try
                    {
                        fileInfo.Delete();
                        Console.WriteLine($"Deleted {fileInfo.Name}");

                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Failed to delete {fileInfo.Name}");

                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the full path of a Directory or defaults to current Directories full path
        /// </summary>
        /// <param name="path">Partial path</param>
        /// <returns>Full Path</returns>
        public static string GetFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return Environment.CurrentDirectory;
            }
            else
            {
                return Path.GetFullPath(path);
            }
        }

        /// <summary>
        /// Delete files in directory
        /// </summary>
        /// <param name="filePattern">Pattern to match with</param>
        public void CleanUpFile(string filePattern)
        {
            foreach (var file in Directory.GetFiles(filePattern))
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
