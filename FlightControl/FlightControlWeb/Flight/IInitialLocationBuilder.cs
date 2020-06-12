using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public interface IInitialLocationBuilder
    {
        /// <summary>
        /// Create an initial location based on the builder.
        /// </summary>
        /// <returns> An initial location based on the builder. </returns>
        public InitialLocation Create();
    }
}
