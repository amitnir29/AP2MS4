using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ITestManager manager = new TestManager();


        // GET: api/Test
        [HttpGet]
        public IEnumerable<TestNum> Get()
        {
            return manager.GetAllNums();
        }

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public TestNum Get(string id)
        {
            return manager.GetNumById(int.Parse(id));
        }

        // POST: api/Test
        [HttpPost]
        public void Post(TestNum value)
        {
            //TestNum x = JsonSerializer.Deserialize<TestNum>(value);
            TestNum x = value;
            manager.AddNum(x);
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //manager.EditNum(new TestNum(id, int.Parse(value)));
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
