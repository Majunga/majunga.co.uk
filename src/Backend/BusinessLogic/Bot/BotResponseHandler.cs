// <copyright file="BotResponseHandler.cs" company="Majunga.co.uk">
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

        /// <summary>
        /// Response with Hero card of the download file
        /// </summary>
        /// <param name="title">Title of Hero Card</param>
        /// <param name="message">Mesage to give</param>
        /// <param name="downloadUri">Download Url</param>
        /// <returns>Task</returns>
        public async Task SendDownloadHeroCard(string title, string message, string downloadUri)
        {
            var cardAction = new CardAction
            {
                Type = "downloadFile",
                Value = downloadUri
            };

            var heroCard = new HeroCard
            {
                Title = title,
                Text = message,
                Tap = cardAction
            };
            var reply = this.activity.CreateReply(string.Empty);
            Attachment heroCardAttachment = heroCard.ToAttachment();
            reply.Attachments.Add(heroCardAttachment);
            await this.connector.Conversations.ReplyToActivityAsync(reply);
        }

        /// <summary>
        /// Response with Hero card
        /// </summary>
        /// <param name="title">Title of Hero Card</param>
        /// <param name="message">Mesage to give</param>
        /// <returns>Task</returns>
        public async Task SendHeroCard(string title, string message)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Text = message
            };
            var reply = this.activity.CreateReply(string.Empty);
            Attachment heroCardAttachment = heroCard.ToAttachment();
            reply.Attachments.Add(heroCardAttachment);
            await this.connector.Conversations.ReplyToActivityAsync(reply);
        }
    }
}