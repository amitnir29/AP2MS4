using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FlightControlWeb.Model;

namespace FlightControlWeb.HTTPServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsModel flightsModel;

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="fm"> The model. </param>
        public FlightsController(IFlightsModel fm)
        {
            flightsModel = fm;
        }


        /// <summary>
        /// Get all flights from the server relative to a given time.
        /// </summary>
        /// <param name="relativeTo"> The time the flihts returned should be relative to. </param>
        /// <returns> All flights from the server relative to a given time. </returns>
        [HttpGet]
        public async Task<IList<Flight.Flight>> Get([FromQuery(Name ="relative_to")] string relativeTo)
        {
            // Indicate if should ask external servers for their flights.
            bool syncAll = Request.Query.ContainsKey("sync_all");

            if (syncAll)
                return await flightsModel.GetAllFlightsSync(DateTime.Parse(relativeTo).ToUniversalTime());
            return await flightsModel.GetAllFlights(DateTime.Parse(relativeTo).ToUniversalTime());
        }



        /// <summary>
        /// Delete a flight.
        /// </summary>
        /// <param name="id"> The id of the flight to delete. </param>
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            await flightsModel.DeleteFlight(id);
        }
    }
}
