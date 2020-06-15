using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.DB;
using FlightControlWeb.Servers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.HTTPServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServersDB serversDB;
        private readonly IFlightsServersDB flightsServersDB;

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="serversDB"> The database stores the uris of the external seervers. </param>
        /// <param name="flightsServersDB"> The database stores the connections between the external
        /// flights and the external servers. </param>
        public ServersController(IServersDB serversDB, IFlightsServersDB flightsServersDB)
        {
            this.serversDB = serversDB;
            this.flightsServersDB = flightsServersDB;
        }


        /// <summary>
        /// Get all the external servers uris stored in the server.
        /// </summary>
        /// <returns> All the external servers uris stored in the server. </returns>
        [HttpGet]
        public async Task<ActionResult<IList<Server>>> Get()
        {
            IList<Server> servers = new List<Server>();
            await foreach (Server server in serversDB.GetAllServers())
            {
                servers.Add(server);
            }

            return Ok(servers);
        }

        
        /// <summary>
        /// Save an external server in the server.
        /// </summary>
        /// <param name="server"> The server to save. </param>
        /// <returns> The result of thhe post action. </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Server server)
        {
            try
            {
                await serversDB.AddServer(server);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        /// <summary>
        /// Delete an external server from the server.
        /// </summary>
        /// <param name="id"> The id of the server to delete. </param>
        /// <returns> The result of the delete action. </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await serversDB.DeleteServer(id);
                await flightsServersDB.DeleteServer(id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
