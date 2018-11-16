LUISResponse luisResponse = await LUIS.GetLUISResult(userInput);

switch (luisResponse.topScoringIntent.intent)
{
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