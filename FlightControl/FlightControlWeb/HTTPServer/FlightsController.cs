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
        private static IFlightsModel flightsModel;

        // GET: api/Flights/relative_to=<DATE_TIME>
        [HttpGet("{relative_to}")]
        public IList<Flight.Flight> Get(string relativeTo)
        {
            return flightsModel.GetAllFlights(relativeTo);
        }


        // GET: api/Flights/relative_to=<DATE_TIME>&sync_all
        [HttpGet("{relative_to}&sync_all")]
        public IList<Flight.Flight> GetSynchAll(string relativeTo)
        {
            return flightsModel.GetAllFlightsSync(relativeTo);
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsModel.DeleteFlight(id);
        }
    }
}
