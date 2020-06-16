using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FlightControlWeb.Model;
using System.Runtime.CompilerServices;

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
        /// <param name="relativeTo"> The time the flights returned should be relative to. </param>
        /// <returns> All flights from the server relative to a given time. </returns>
        [HttpGet]
        public async Task<ActionResult<IList<Flight.Flight>>> Get(
            [FromQuery(Name ="relative_to")] string relativeTo)
        {
            // Indicate if should ask external servers for their flights.
            bool syncAll = Request.Query.ContainsKey("sync_all");

            IList<Flight.Flight> res;
            DateTime relativeToTime = DateTime.Parse(relativeTo).ToUniversalTime();

            if (syncAll)
                res = await flightsModel.GetAllFlightsSync(relativeToTime);
            else
                res = await flightsModel.GetAllFlights(relativeToTime);

            return Ok(res);
        }



        /// <summary>
        /// Delete a flight.
        /// </summary>
        /// <param name="id"> The id of the flight to delete. </param>
        /// <returns> The result of the delete action. </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await flightsModel.DeleteFlight(id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
