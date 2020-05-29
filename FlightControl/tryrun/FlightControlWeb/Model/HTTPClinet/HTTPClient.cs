using FlightControlWeb.Flight;
using FlightControlWeb.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightControlWeb.Model.HTTPClinet
{
    public class HTTPClient : IHTTPClient
    {
        private string requestsUrl;
        System.Net.Http.HttpClient client;

        public HTTPClient(Server server)
        {
            requestsUrl = server.Url + "/api/FlightPlan";

            client = new System.Net.Http.HttpClient();
        }


        public async Task<FlightPlan> GetFlightPlan(string id)
        {
            string uri = requestsUrl + "/" + id;

            string content = await client.GetStringAsync(uri);

            return JsonSerializer.Deserialize<FlightPlan>(content);
        }


        public async Task<IList<Flight.Flight>> GetFlights(string relativeTo)
        {
            string uri = requestsUrl + "?relative_to=" + relativeTo;

            string content = await client.GetStringAsync(uri);

            return JsonSerializer.Deserialize<List<Flight.Flight>>(content);
        }
    }
}
