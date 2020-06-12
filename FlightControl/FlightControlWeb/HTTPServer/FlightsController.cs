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

        public FlightsController(IFlightsModel fm)
        {
            flightsModel = fm;
        }

        // GET: api/Flights/relative_to=<DATE_TIME>
        [HttpGet]
        public async Task<IList<Flight.Flight>> Get([FromQuery(Name ="relative_to")] string relativeTo)
        {
            bool syncAll = Request.Query.ContainsKey("sync_all");

            if (syncAll)
                return await flightsModel.GetAllFlightsSync(DateTime.Parse(relativeTo));
            return await flightsModel.GetAllFlights(DateTime.Parse(relativeTo));
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            await flightsModel.DeleteFlight(id);
        }
    }
}
