using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Servers
{
    public class FlightServer
    {
        private string flightid;
        public string FlightId
        {
            get => flightid;
            set => flightid = value;
        }


        private string serverid;
        public string ServerId
        {
            get => serverid;
            set => serverid = value;
        }


        public FlightServer(string flightid, string serverid)
        {
            FlightId = flightid;
            ServerId = serverid;
        }
    }
}
