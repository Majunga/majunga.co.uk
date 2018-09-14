// <copyright file="MessageController.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BotDot.BusinessLogic.Bot;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Bot.Connector;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Bot Messages controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="configuration">Configuration of Application</param>
        public MessagesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// POST Message method
        /// </summary>
        /// <param name="activity">Activatity object</param>
        /// <returns>Action Result</returns>
        [Authorize(Roles = "Bot")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                // MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
                var appCredentials = new MicrosoftAppCredentials(this.configuration);
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl), appCredentials);

                var arguments = new ArguementsHandler(activity.Text);
                if (arguments.CanAction())
                {
                    var command = arguments.GetCommand();

                    switch (command)
                    {
                        case CommandHandler.Commands.Download:
                            break;
                        default:
                            break;
                    }
                }

                // return our reply to the user
                //var reply = activity.CreateReply("HelloWorld");
                //await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                // HandleSystemMessage(activity);
            }

            return this.Ok();
        }
    }
}
