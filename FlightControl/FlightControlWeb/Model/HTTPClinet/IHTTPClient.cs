using FlightControlWeb.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model.HTTPClinet
{
    interface IHTTPClient
    {
        /// <summary>
        /// Get a flight plan from an external server.
        /// </summary>
        /// <param name="id"> The id of the flight plan to get. </param>
        /// <returns> The requested flight plan if the server returned it.
        /// Otherwise, return null. </returns>
        public Task<FlightPlan> GetFlightPlan(string id);


        /// <summary>
        /// Get all the flights from an external server.
        /// </summary>
        /// <param name="relativeTo"> The time that the query is relative to. </param>
        /// <returns> The flights if the query succeeded, null otherwise. </returns>
        public Task<IList<Flight.Flight>> GetFlights(string relativeTo);
    }
}
