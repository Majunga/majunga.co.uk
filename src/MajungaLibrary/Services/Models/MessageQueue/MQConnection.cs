// <copyright file="MQConnection.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace MajungaLibrary.BusinessLogic.Services.Models.MessageQueue
{
    /// <summary>
    /// Message Queue connection model
    /// </summary>
    public class MQConnection
    {
        /// <summary>
        /// Gets or sets host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets channel
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets routingKey
        /// </summary>
        public string RoutingKey { get; set; }
    }
}
