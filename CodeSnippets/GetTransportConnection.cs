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