using FlightControlWeb.Flight;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    public interface IFlightsDB
    {
        /// <summary>
        /// Add a flight plan to the database.
        /// </summary>
        /// <param name="flightPlan"> The flight plan to add. </param>
        public Task AddFlightPlan(FlightPlan flightPlan);

        /// <summary>
        /// Get a flight plan from the database.
        /// </summary>
        /// <param name="id"> The id of the flight plan. </param>
        /// <returns> The requested flightplan. </returns>
        public Task<FlightPlan> GetFlightPlan(string id);

        /// <summary>
        /// Delete a flight plan from the data base.
        /// </summary>
        /// <param name="id"> The id of the flight plan to delete. </param>
        public Task DeleteFlightPlan(string id);

        /// <summary>
        /// Get an iterator to iterate over all the flight plans in the data base.
        /// </summary>
        /// <returns> An iterator to iterate over all the flight plans in the data base. </returns>
        public IAsyncEnumerable<FlightPlan> GetAllFlightPlans();
    }
}
