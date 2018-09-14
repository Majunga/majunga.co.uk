// <copyright file="BotResponses.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.BusinessLogic.Bot
{
    using System.Threading.Tasks;
    using Microsoft.Bot.Connector;

    /// <summary>
    /// Response to user
    /// </summary>
    public class BotResponseHandler
    {
        private ConnectorClient connector;
        private Activity activity;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotResponseHandler"/> class.
        /// </summary>
        /// <param name="connector">Connector to User</param>
        /// <param name="activity">User Activity</param>
        public BotResponseHandler(ConnectorClient connector, Activity activity)
        {
            this.connector = connector;
            this.activity = activity;
        }

        /// <summary>
        /// Send Message to User
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <returns>Task</returns>
        public async Task SendMessage(string message)
        {
            await this.connector.Conversations.ReplyToActivityAsync(this.activity.CreateReply(message));
        }
    }
}