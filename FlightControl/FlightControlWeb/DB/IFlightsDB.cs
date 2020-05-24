﻿using FlightControlWeb.Flight;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    interface IFlightsDB
    {
        public Task<FlightPlan> PostFlightPlan(FlightPlan flightPlan);

        public Task<FlightPlan> GetFlightPlan(string id);

        public void DeleteFlightPlan(string id);

        public Task<IEnumerable<FlightPlan>> GetIterator();
    }
}