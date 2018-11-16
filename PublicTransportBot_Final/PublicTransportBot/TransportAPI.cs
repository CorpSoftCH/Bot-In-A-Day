using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace PublicTransportBot
{
    public class TransportAPI
    {
        public static async Task<TransportResponse> GetConnections(string fromCity, string toCity)
        {
                string apiUrl = $"http://transport.opendata.ch/v1/connections?from={fromCity}&to={toCity}";

                string jsonResult;

                using (WebClient client = new WebClient())
                {
                    jsonResult = await client.DownloadStringTaskAsync(apiUrl).ConfigureAwait(false);
                }

                TransportResponse jsonResponseObject = JsonConvert.DeserializeObject<TransportResponse>(jsonResult);

                return jsonResponseObject;
        }
    }
}
