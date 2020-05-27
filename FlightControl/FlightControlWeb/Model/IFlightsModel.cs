using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FlightControlWeb.Flight;

namespace FlightControlWeb.Model
{
    public interface IFlightsModel
    {
        public Task<FlightPlan> GetFlightPlan(string id);

        public Task<IList<Flight.Flight>> GetAllFlights(DateTime relativeTo);

        public Task<IList<Flight.Flight>> GetAllFlightsSync(DateTime relativeTo);

        public Task AddFlightPlan(FlightPlan plan);

        public Task DeleteFlight(string id);
    }
}
