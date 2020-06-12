using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public interface IFlightPlanBuilder
    {
        /// <summary>
        /// Create a flight plan based on the builder.
        /// </summary>
        /// <returns> A flight plan based on the builder. </returns>
        public FlightPlan Create();
    }
}
