using FlightControlWeb.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model
{
    interface IFlightCalculator
    {
        /// <summary>
        /// Generate a flight from a given flight plan and a time.
        /// </summary>
        /// <param name="plan"> The flight plan to generate from. </param>
        /// <param name="relativeTo"> The time
        /// (to calculate the plane's position). </param>
        /// <param name="isExternal"> Indicate if the flight is given from
        /// an external server. </param>
        /// <returns> A corresponding flight or null if
        /// there is no flight at the given time. </returns>
        public Flight.Flight CreateFlightFromPlan(FlightPlan plan, DateTime relativeTo, bool isExternal);
    }
}
