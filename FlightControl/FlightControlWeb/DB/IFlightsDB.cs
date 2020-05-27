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
<<<<<<< HEAD

        public Task<ActionResult<FlightPlan>> PostFlightPlan(FlightPlan flightPlan);

        public Task<ActionResult<FlightPlan>> GetFlightPlan(string id);

        public void DeleteFlightPlan(string id);

        public Task<ActionResult<IEnumerable<FlightPlan>>> GetIterator();

=======
        public Task PostFlightPlan(FlightPlan flightPlan);

        public Task<FlightPlan> GetFlightPlan(string id);

        public Task DeleteFlightPlan(string id);

        public IAsyncEnumerable<FlightPlan> GetIterator();
<<<<<<< HEAD
    }
}
=======
        //public IAsyncEnumerable<FlightPlan> GetIterator();
>>>>>>> server
    }
}
>>>>>>> server
