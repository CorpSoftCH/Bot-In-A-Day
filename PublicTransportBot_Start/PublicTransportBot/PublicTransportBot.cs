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
                string userReplyString = "Hi User!";


                // return our reply to the user
                Activity defaultReply = activity.CreateReply(userReplyString);
                await turnContext.SendActivityAsync(defaultReply);
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}
