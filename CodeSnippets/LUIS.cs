using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PublicTransportBot
{
    public class LUIS
    {
        public static async Task<LUISResponse> GetLUISResult(string Query)
        {
            Query = Uri.EscapeDataString(Query);
            LUISResponse Data = new LUISResponse();
            using (HttpClient client = new HttpClient())
            {
                string RequestURI = "###ENTER YOUR LUIS APP CONNECTION STRING HERE###" + Query;

                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<LUISResponse>(JsonDataResponse);
                }
            }
            return Data;
        }
    }
}
