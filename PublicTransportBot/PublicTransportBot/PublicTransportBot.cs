// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace PublicTransportBot
{
    public class PublicTransportBot : IBot
    {
        private readonly BotAccessors _accessors;
        private readonly ILogger _logger;

        public PublicTransportBot(BotAccessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<PublicTransportBot>();
            _logger.LogTrace("PublicTransport Bot turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
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

                            userReplyString = $@"Ich habe die folgenden Verbindungen für Dich gefunden von {connectionData.from.name} nach {connectionData.to.name}:";

                            int counter = 0;
                            foreach (var connection in connectionData.connections)
                            {
                                counter++;

                                string fromStationName = connection.from.station.name;
                                string fromPlatform = connection.from.platform;
                                DateTime fromDeparture = connection.from.departure;
                                string toStationName = connection.to.station.name;
                                string toPlatform = connection.to.platform;
                                DateTime toArrival = connection.to.arrival;
                                string duration = connection.duration;
                                string products = string.Join(",", connection.products);
                                string changes = connection.transfers.ToString();

                                userReplyString += "\n\r\n\r";
                                userReplyString += $"Verbindung {counter}:\n\r";
                                userReplyString += $"Abfahrt {fromStationName}, Gleis {fromPlatform} um {fromDeparture.ToString("dd.MM.yyyy HH:mm")}\n\r";
                                userReplyString += $"Ankunft {toStationName}, Gleis {toPlatform} um {toArrival.ToString("dd.MM.yyyy HH:mm")}\n\r";
                                userReplyString += $"Dauer: {duration}, Umsteigen: {changes}, Züge: {products}\n\r";
                            }

                            // return our reply to the user
                            Activity connectionsReply = activity.CreateReply(userReplyString);
                            await turnContext.SendActivityAsync(connectionsReply);

                        }
                        catch (Exception e)
                        {
                            Activity errorReply = activity.CreateReply(e.Message);
                            await turnContext.SendActivityAsync(errorReply);
                        }

                        break;
                    case "Greeting":
                        userReplyString = $@"Hallo {senderName}, Du siehst heute aber speziell gut aus!";

                        // return our reply to the user
                        Activity greetingReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(greetingReply);
                        break;
                    case "Insult":
                        userReplyString = $@"{senderName}, es scheint mir Du hast heute einen Bad Hair Day!";

                        // return our reply to the user
                        Activity insultReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(insultReply);
                        break;
                    case "None":
                        userReplyString = "Kein Intent, Du intentloser Mensch! Hi trotzdem;-)";

                        // return our reply to the user
                        Activity noneReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(noneReply);
                        break;
                    default:
                        userReplyString = "Kein Intent, Du intentloser Mensch! Hi trotzdem;-)";

                        // return our reply to the user
                        Activity defaultReply = activity.CreateReply(userReplyString);
                        await turnContext.SendActivityAsync(defaultReply);
                        break;
                }
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}
