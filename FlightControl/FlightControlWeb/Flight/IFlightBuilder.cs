using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public interface IFlightBuilder
    {
        /// <summary>
        /// Create a flight.
        /// </summary>
        /// <returns> FLight based on the builder. </returns>
        public Flight Create();
    }
}
