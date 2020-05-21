using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FlightControlWeb.Flight;

namespace FlightControlWeb.Model
{
    public interface IFlightsModel
    {
        public FlightPlan GetFlightPlan(string id);

        public IList<Flight.Flight> GetAllFlights();

        public IList<Flight.Flight> GetAllFlightsSync();

        public void AddFlightPlan(FlightPlan plan);

        public void DeleteFlight(string id);
    }
}
