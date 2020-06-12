﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    interface IIDGenerator
    {
        /// <summary>
        /// Generate an ID.
        /// </summary>
        /// <returns> A strinf representing an id, </returns>
        public string GenerateID();
    }
}
