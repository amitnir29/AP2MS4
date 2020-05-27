using FlightControlWeb.DB;
using FlightControlWeb.Flight;
using FlightControlWeb.Model.HTTPClinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model
    {
        public class FlightsHandler: IFlightsModel
        {

            private IFlightsDB flightsDB = new MyFlightsDB();
            private IFlightsServersDB flightsServersDB = new MyFlightsServersDB();
            private IServersDB serversDB = new MyServersDB();
            private IFlightCalculator calculator = new FlightCalculator();

            /// <summary>
            /// Get a flight plan from the database and return it.
            /// </summary>
            /// <param name="id"> The id of the flight plan. </param>
            /// <returns> The flight plan. </returns>
            public async Task<FlightPlan> GetFlightPlan(string id)
            {
                var local = await flightsDB.GetFlightPlan(id);

                if (local != null)
                    return local;

                var relevantServer = await flightsServersDB.GetFlightServer(id);
                var server = await serversDB.GetServer(relevantServer.ServerId);

                IHTTPClient client = new HTTPClient(server);

                return await client.GetFlightPlan(id);
            }


            /// <summary>
            /// Get all flights from this server.
            /// </summary>
            /// <param name="relativeTo"> The current time at the user's </param>
            /// <returns> All the flights. </returns>
            public async Task<IList<Flight.Flight>> GetAllFlights(DateTime relativeTo)
            {
                IList<Flight.Flight> flights = new List<Flight.Flight>();

                await foreach (var flightPlan in flightsDB.GetIterator())
                {
                    flights.Add(calculator.CreateFlightFromPlan(flightPlan, relativeTo, false));
                }

                return flights;
            }


            /// <summary>
            /// Get all the flights from this server and other servers.
            /// </summary>
            /// <param name="relativeTo"> Time at the student's. </param>
            /// <returns> All the flights. </returns>
            public async Task<IList<Flight.Flight>> GetAllFlightsSync(DateTime relativeTo)
            {
                Task<IList<Flight.Flight>> localFlights = GetAllFlights(relativeTo);

                IList<Task<IList<Flight.Flight>>> externalFlights = new List<Task<IList<Flight.Flight>>>();

                await foreach (var server in serversDB.GetIterator())
                {
                    HTTPClient client = new HTTPClient(server);
                    externalFlights.Add(client.GetFlights(relativeTo.ToString("yyyy-MM-ddTHH:mm:ssZ")));
                }

                await localFlights;

                var temp = localFlights;

                foreach (var list in externalFlights)
                {
                    temp.Result.Concat(list.Result);
                }

                return temp.Result;
            }



            /// <summary>
            /// Add a flight plan to the database.
            /// </summary>
            /// <param name="plan"> The flight plan to add. </param>
            public async Task AddFlightPlan(FlightPlan plan)
            {
                await flightsDB.PostFlightPlan(plan);
            }


            /// <summary>
            /// Delete a flight plan from the data base.
            /// </summary>
            /// <param name="id"> The id of the flight to delete. </param>
            public async Task DeleteFlight(string id)
            {
                await flightsDB.DeleteFlightPlan(id);
            }
        }
    }
