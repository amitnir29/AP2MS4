using FlightControlWeb.DB;
using FlightControlWeb.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model
{
    public class FLightsModel : IFlightsModel
    {

        private IFlightsDB dataBase;

        /// <summary>
        /// Get a flight plan from the database and return it.
        /// </summary>
        /// <param name="id"> The id of the flight plan. </param>
        /// <returns> The flight plan. </returns>
        public FlightPlan GetFlightPlan(string id)
        {
            var res = dataBase.GetFlightPlan(id);
            res.Wait();
            return res.Result;
        }


        private CreateFlightFromPlan(FlightPlan plan, DateTime relativeTo)
        {
        }


        /// <summary>
        /// Get all flights from this server.
        /// </summary>
        /// <param name="relativeTo"> The current time at the user's </param>
        /// <returns> All the flights. </returns>
        public Task<IList<Flight.Flight>> GetAllFlights(DateTime relativeTo)
        {

        }


        /// <summary>
        /// Get all the flights from this server and other servers.
        /// </summary>
        /// <param name="relativeTo"> Time at the student's. </param>
        /// <returns> All the flights. </returns>
        public IList<Flight.Flight> GetAllFlightsSync(DateTime relativeTo);



        /// <summary>
        /// Add a flight plan to the database.
        /// </summary>
        /// <param name="plan"> The flight plan to add. </param>
        public void AddFlightPlan(FlightPlan plan)
        {
            dataBase.PostFlightPlan(plan);
        }


        /// <summary>
        /// Delete a flight plan from the data base.
        /// </summary>
        /// <param name="id"> The id of the flight to delete. </param>
        public void DeleteFlight(string id)
        {
            dataBase.DeleteFlightPlan(id);
        }
    }
}
