// <copyright file="ProcessHelper.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// Helper to Run Processes
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// Run Process
        /// </summary>
        /// <param name="filename">Name of file to run</param>
        /// <param name="arguements">Arguments</param>
        /// <returns>Task</returns>
        public async Task Run(string filename, string arguements)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = filename,
                    Arguments = arguements
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception(await process.StandardError.ReadToEndAsync());
            }
        }
    }
}
