using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public interface IFlightStatusBuilder
    {
        /// <summary>
        /// Create a flight status based on the builder.
        /// </summary>
        /// <returns> A flight status based on the builder. </returns>
        public FlightStatus Create();
    }
}
