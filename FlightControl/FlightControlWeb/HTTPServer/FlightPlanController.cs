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

        public FlightPlanController(IFlightsModel fm)
        {
            flightsModel = fm;
        }


        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<FlightPlan> Get(string id)
        {
            return await flightsModel.GetFlightPlan(id);
        }

        // POST: api/FlightPlan
        [HttpPost]
        public async Task Post([FromBody] FlightPlan plan)
        {
            /*Console.WriteLine("hi");
            Console.WriteLine(plan.GetID());
            Console.WriteLine("hi2");*/
            await flightsModel.AddFlightPlan(plan);
        }
    }
}
