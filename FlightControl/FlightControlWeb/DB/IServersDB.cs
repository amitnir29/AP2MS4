using FlightControlWeb.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    public interface IServersDB
    {
        /// <summary>
        /// Add a server to the database.
        /// </summary>
        /// <param name="server"> A server object to add. </param>
        public Task AddServer(Server server);

        /// <summary>
        /// Get a server from the database.
        /// </summary>
        /// <param name="id"> The id of the requested server. </param>
        /// <returns> A serverobject, as requested. </returns>
        public Task<Server> GetServer(string id);

        /// <summary>
        /// Delete a server from the database.
        /// </summary>
        /// <param name="id"> The id of the server to delete. </param>
        public Task DeleteServer(string id);

        /// <summary>
        /// Get an iterator to iterate over all the servers in the database.
        /// </summary>
        /// <returns> An iterator to iterate over all t he servers in the database. </returns>
        public IAsyncEnumerable<Server> GetAllServers();
    }
}
