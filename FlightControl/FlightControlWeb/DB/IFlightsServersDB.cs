using FlightControlWeb.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    interface IFlightsServersDB
    {
        public Task PostFlightServer(FlightServer fs);

        public Task<FlightServer> GetFlightServer(string flightid);

        public Task DeleteServer(string serverid);

        public IAsyncEnumerable<FlightServer> GetServerIterator(string serverid);
    }
}
