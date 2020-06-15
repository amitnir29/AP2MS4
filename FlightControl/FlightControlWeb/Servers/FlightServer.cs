using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Servers
{
    /// <summary>
    /// The connection between a flight and the server in charge of that flight.
    /// </summary>
    public class FlightServer
    {
        private string flightid;
        /// <summary>
        /// The id of the flight.
        /// </summary>
        public string FlightId
        {
            get => flightid;
            set => flightid = value;
        }


        private string serverid;
        /// <summary>
        /// The id of the server.
        /// </summary>
        public string ServerId
        {
            get => serverid;
            set => serverid = value;
        }


        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="flightid"> The id of the flight. </param>
        /// <param name="serverid"> The id of the server. </param>
        public FlightServer(string flightid, string serverid)
        {
            FlightId = flightid;
            ServerId = serverid;
        }
    }
}
