using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class IDGenerator : IIDGenerator
    {
        private string prefix;


        public IDGenerator(string prefix)
        {
            this.prefix = prefix;
        }


        public string GenerateID()
        {
            return prefix + (new Random()).Next();
        }
    }
}
