// <copyright file="MessagesController.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace BotDot.Controllers
{
    using BotDot.BusinessLogic.Bot;
    using BotDot.BusinessLogic.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Bot.Connector;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Bot Messages controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IConfiguration configuration;
        private IYoutubeDownload download;
        private IVideoConverter videoConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="configuration">Configuration of Application</param>
        /// <param name="download">Downloads files from Internet</param>
        /// <param name="videoConverter">Video conversion</param>
        public MessagesController(IConfiguration configuration, IYoutubeDownload download, IVideoConverter videoConverter)
        {
            Console.WriteLine("Entered MessageController");

            this.configuration = configuration;
            this.download = download;
            this.videoConverter = videoConverter;
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
            try
            {
                Console.WriteLine("Entered Message POST Action");


                if (activity.Type == ActivityTypes.Message)
                {
                    Console.WriteLine($"Message Received: {activity.Text}");
                    Console.WriteLine("Setting up Connection");
                    MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
                    var appCredentials = new MicrosoftAppCredentials(this.configuration);
                    var connector = new ConnectorClient(new Uri(activity.ServiceUrl), appCredentials);
                    Console.WriteLine("Finished setting up");
                    var messagetest = activity.CreateReply("Replying");
                    Console.WriteLine("Message created");
                    connector.Conversations.ReplyToActivityAsync(messagetest);
                    Console.WriteLine("Replied");
                    var arguments = new ArguementsHandler(activity.Text);
                    if (arguments.CanAction())
                    {
                        var command = arguments.GetCommand();
                        var responseHandler = new BotResponseHandler(connector, activity);
                        switch (command)
                        {
                            case CommandHandler.Commands.Download:
                                await new CommandHandler(this.download, this.videoConverter).DownloadCommand(arguments, responseHandler);
                                break;
                            default:
                                var message = "**Bot Help**\n";
                                message += "Usage: bot download [Optional Commands] [Youtube URL]\n";
                                message += "Where [Commands] is one of:\n";
                                message += "*--start HH:MM:SS*\n";
                                message += "*--end HH:MM:SS*\n";
                                await responseHandler.SendMessage(message);
                                break;
                        }
                    }

                    // return our reply to the user
                    // var reply = activity.CreateReply("HelloWorld");
                    // await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else
                {
                    // HandleSystemMessage(activity);
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} ");
                throw;
            }
        }

    }
}
