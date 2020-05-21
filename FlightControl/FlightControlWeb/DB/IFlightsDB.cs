using FlightControlWeb.Flight;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    interface IFlightsDB
    {

        public Task<ActionResult<FlightPlan>> PostFlightPlan(FlightPlan flightPlan);

        public Task<ActionResult<FlightPlan>> GetFlightPlan(string id);

        public void DeleteFlightPlan(string id);

        public Task<ActionResult<IEnumerable<FlightPlan>>> GetIterator();

    }
}
