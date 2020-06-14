using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    interface IIDGenerator
    {
        /// <summary>
        /// Generate an id.
        /// </summary>
        /// <returns> The generated id. </returns>
        public string GenerateID();
    }
}
