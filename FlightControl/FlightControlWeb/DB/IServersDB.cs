using FlightControlWeb.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    interface IServersDB
    {
        public Task PostServer(Server server);

        public Task<Server> GetServer(string id);

        public Task DeleteServer(string id);

        public IAsyncEnumerable<Server> GetIterator();
    }
}
