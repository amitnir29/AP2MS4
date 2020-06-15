using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Flight;
using FlightControlWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.HTTPServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private readonly IFlightsModel flightsModel;

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="fm"> The model. </param>
        public FlightPlanController(IFlightsModel fm)
        {
            flightsModel = fm;
        }


        /// <summary>
        /// Get a flight plan.
        /// </summary>
        /// <param name="id"> The id of the flight plan to get. </param>
        /// <returns> The requested flight plan. </returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<FlightPlan> Get(string id)
        {
            return await flightsModel.GetFlightPlan(id);
        }

        
        /// <summary>
        /// A controller for the post method.
        /// Post a new flight plan into the server.
        /// </summary>
        /// <param name="plan"> The posted flight plan. </param>
        [HttpPost]
        public async Task Post([FromBody] FlightPlan plan)
        {
            await flightsModel.AddFlightPlan(plan);
        }
    }
}
