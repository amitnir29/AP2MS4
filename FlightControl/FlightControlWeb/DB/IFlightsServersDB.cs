using FlightControlWeb.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    public interface IFlightsServersDB
    {
        /// <summary>
        /// Add a flight-server object to the data base.
        /// </summary>
        /// <param name="fs"> The flight-server object to add. </param>
        public Task AddFlightServer(FlightServer fs);

        /// <summary>
        /// Get a flight-server object from the database.
        /// </summary>
        /// <param name="flightid"> The id of the requested flight-server object. </param>
        /// <returns> The flight-server object. </returns>
        public Task<FlightServer> GetFlightServer(string flightid);

        /// <summary>
        /// Delete a flight-server object from the database.
        /// </summary>
        /// <param name="serverid"> The id of the flight-server object to delete. </param>
        public Task DeleteServer(string serverid);
    }
}
