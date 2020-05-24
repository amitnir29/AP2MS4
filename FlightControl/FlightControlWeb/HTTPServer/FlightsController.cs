﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FlightControlWeb.Model;

namespace FlightControlWeb.HTTPServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private static IFlightsModel flightsModel;

        // GET: api/Flights/relative_to=<DATE_TIME>
        [HttpGet("relative_to={relativeTo}")]
        public IList<Flight.Flight> Get(string relativeTo)
        {
            var res = flightsModel.GetAllFlights(DateTime.Parse(relativeTo));
            res.Wait();
            return res.Result;
        }


        // GET: api/Flights/relative_to=<DATE_TIME>&sync_all
        [HttpGet("relative_to={relativeToo}&sync_all")]
        public IList<Flight.Flight> GetSynchAll(string relativeTo)
        {
            return flightsModel.GetAllFlightsSync(DateTime.Parse(relativeTo));
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsModel.DeleteFlight(id);
        }
    }
}
