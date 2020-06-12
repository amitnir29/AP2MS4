using FlightControlWeb.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model
{
    interface IFlightCalculator
    {
        public Flight.Flight CreateFlightFromPlan(FlightPlan plan, DateTime relativeTo, bool isExternal);
    }
}
