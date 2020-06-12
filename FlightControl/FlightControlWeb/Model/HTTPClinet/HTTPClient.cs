﻿using FlightControlWeb.Flight;
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
        private string requestsUrl;
        System.Net.Http.HttpClient client;

        public HTTPClient(Server server)
        {
            requestsUrl = server.Url;

            client = new System.Net.Http.HttpClient();
        }


        public async Task<FlightPlan> GetFlightPlan(string id)
        {
            string uri = requestsUrl  + "/api/FlightPlan/" + id;

            try
            {

                string content = await client.GetStringAsync(uri);

                FlightPlanBuilder converted = JsonConvert.DeserializeObject<FlightPlanBuilder>(content);

                return converted.Create();
            }
            catch(Exception)
            {
                return null;
            }
        }


        public async Task<IList<Flight.Flight>> GetFlights(string relativeTo)
        {
            string uri = requestsUrl + "/api/Flights/?relative_to=" + relativeTo;

            try
            {

                string content = await client.GetStringAsync(uri);

                IList<FlightBuilder> converted = JsonConvert.DeserializeObject<IList<FlightBuilder>>(content);

                IList<Flight.Flight> flights = new List<Flight.Flight>();

                foreach (IFlightBuilder builder in converted)
                {
                    Flight.Flight flight = builder.Create();
                    flights.Add(flight);
                }

                return flights;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
