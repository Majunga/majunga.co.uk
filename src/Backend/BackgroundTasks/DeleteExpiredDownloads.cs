// <copyright file="DeleteExpiredDownloads.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace Backend.BackgroundTasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MajungaLibrary.Helpers;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Deletes Expired Downloads, so server storages isn't filleds
    /// </summary>
    public class DeleteExpiredDownloads : IHostedService, IDisposable
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteExpiredDownloads"/> class.
        /// </summary>
        /// <param name="env">Environment Param</param>
        public DeleteExpiredDownloads(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env ?? throw new ArgumentNullException(nameof(env));
        }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Clear up files
        /// </summary>
        /// <param name="state">Static of Service</param>
        public void DoWork(object state)
        {
            FileHelper.DeleteExpiredFile($"{this.env.WebRootPath}/static");
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}
