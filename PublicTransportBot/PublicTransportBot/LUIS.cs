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
                string RequestURI = "https://westeurope.api.cognitive.microsoft.com/luis/v2.0/apps/fb2f1043-7f9e-421c-82dd-f9cfe5f8072e?subscription-key=1003e79ba60c47eeac4e10c4a662a129&timezoneOffset=60&q=" + Query;

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
