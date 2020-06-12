using FlightControlWeb.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model.HTTPClinet
{
    interface IHTTPClient
    {
        public Task<FlightPlan> GetFlightPlan(string id);

        public Task<IList<Flight.Flight>> GetFlights(string relativeTo);
    }
}
