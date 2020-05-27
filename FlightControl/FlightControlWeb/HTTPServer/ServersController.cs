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
        private IServersDB dataBase = new MyServersDB();

        // GET: api/Servers
        [HttpGet]
        public async Task<IList<Server>> Get()
        {
            return await dataBase.GetAllServers();
        }

        // POST: api/Servers
        [HttpPost]
        public async Task Post([FromBody] Server server)
        {
            await dataBase.PostServer(server);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await dataBase.DeleteServer(id);
        }
    }
}
