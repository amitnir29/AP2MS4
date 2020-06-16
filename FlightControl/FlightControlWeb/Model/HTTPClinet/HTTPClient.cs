using FlightControlWeb.Flight;
using FlightControlWeb.Servers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightControlWeb.Model.HTTPClinet
{
    public class HTTPClient : IHTTPClient
    {
        private readonly string requestsUrl;
        private readonly System.Net.Http.HttpClient client;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="server"> The server to make all the connections with. </param>
        public HTTPClient(Server server)
        {
            requestsUrl = server.Url;

            client = new System.Net.Http.HttpClient();
        }


        /// <summary>
        /// Get a flight plan from an external server.
        /// </summary>
        /// <param name="id"> The id of the flight plan to get. </param>
        /// <returns> The requested flight plan if the server returned it.
        /// Otherwise, return null. </returns>
        public async Task<FlightPlan> GetFlightPlan(string id)
        {
            string uri = requestsUrl  + "/api/FlightPlan/" + id;

            try
            {
                // Get the content from the external server.
                string content = await client.GetStringAsync(uri);

                // Serialize it to a flight plan object.
                FlightPlan converted = JsonConvert.DeserializeObject<FlightPlan>(content);

                return converted;
            }
            catch(Exception)
            {
                // If failed, return null.
                return null;
            }
        }


        /// <summary>
        /// Get all the flights from an external server.
        /// </summary>
        /// <param name="relativeTo"> The time that the query is relative to. </param>
        /// <returns> The flights if the query succeeded, null otherwise. </returns>
        public async Task<IList<Flight.Flight>> GetFlights(string relativeTo)
        {
            string uri = requestsUrl + "/api/Flights/?relative_to=" + relativeTo;

            try
            {
                // Get the content from the external server.
                string content = await client.GetStringAsync(uri);

                // Serialize it to a flight plan object.
                IList<Flight.Flight> converted =
                    JsonConvert.DeserializeObject<List<Flight.Flight>>(content);

                return converted;
            }
            catch (Exception)
            {
                // If failed, return null.
                return null;
            }
        }
    }
}
