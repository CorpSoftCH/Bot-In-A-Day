// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PublicTransportBot
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class PublicTransportBot : IBot
    {
        private readonly EchoBotAccessors _accessors;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicTransportBot"/> class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public PublicTransportBot(EchoBotAccessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<PublicTransportBot>();
            _logger.LogTrace("EchoBot turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {

                var activity = turnContext.Activity;

                var userInput = activity.Text;
                string senderName = activity.From.Name;
                string userReplyString;

                LUISResponse luisResponse = await LUIS.GetLUISResult(userInput);

                switch (luisResponse.topScoringIntent.intent)
                {
                    case "GetTransportConnection":

                        try
                        {

                            var entities = luisResponse.entities;

                            var cityFromEntity = entities.Where(c => c.type == "City::From").FirstOrDefault();
                            var cityToEntity = entities.Where(c => c.type == "City::To").FirstOrDefault();

                            TransportResponse connectionData = await TransportAPI.GetConnections(cityFromEntity.entity, cityToEntity.entity);




                        }
                        catch
                        {

                        }

                        break;
                    case "Greeting":
                        userReplyString = $@"Hello Dear {senderName}, you look especially good today!";

                        // return our reply to the user
                        Activity greetingReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(greetingReply);
                        break;
                    case "Insult":
                        userReplyString = $@"{senderName}, it seems you are having a bad hair day today!";

                        // return our reply to the user
                        Activity insultReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(insultReply);
                        break;
                    case "None":
                        userReplyString = "No Intent, you intentless Human! Hi though ;-)";

                        // return our reply to the user
                        Activity noneReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(noneReply);
                        break;
                    default:
                        userReplyString = "No Intent, you intentless Human! Hi though ;-)";

                        // return our reply to the user
                        Activity defaultReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(defaultReply);
                        break;
                }

                // Echo back to the user whatever they typed.
                var responseMessage = $"Turn 1: You sent '{turnContext.Activity.Text}'\n";
                await turnContext.SendActivityAsync(responseMessage);
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}
