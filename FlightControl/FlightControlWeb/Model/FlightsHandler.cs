using FlightControlWeb.DB;
using FlightControlWeb.Flight;
using FlightControlWeb.Model.HTTPClinet;
using FlightControlWeb.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model
    {
        public class FlightsHandler: IFlightsModel
        {

            private readonly IFlightsDB flightsDB;
            private readonly IFlightsServersDB flightsServersDB;
            private readonly IServersDB serversDB;
            private IFlightCalculator calculator = new FlightCalculator();

            public FlightsHandler(IFlightsDB fpdb, IFlightsServersDB fsdb, IServersDB sdb)
            {
                flightsDB = fpdb;
                flightsServersDB = fsdb;
                serversDB = sdb;
            }


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
                if (relevantServer != null) {
                    var server = await serversDB.GetServer(relevantServer.ServerId);

                    IHTTPClient client = new HTTPClient(server);

                    return await client.GetFlightPlan(id);
                }

                return null;
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
                    var res = calculator.CreateFlightFromPlan(flightPlan, relativeTo, false);
                    if (res != null)
                        flights.Add(res);
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
                // Get first all local flights.
                Task<IList<Flight.Flight>> localFlights = GetAllFlights(relativeTo);

                // Create a list contains lists 
                IList<Task<IList<Flight.Flight>>> externalFlights = new List<Task<IList<Flight.Flight>>>();

                IList<Task> serversUpdates = new List<Task>();

                await foreach (Server server in serversDB.GetIterator())
                {
                    HTTPClient client = new HTTPClient(server);
                    var externals = client.GetFlights(relativeTo.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                    var res = await externals;

                    if (res == null)
                        continue;

                    foreach(var flight in res)
                    {
                        flight.IsExternal = true;
                        var serverUpdate = flightsServersDB.PostFlightServer(new Servers.FlightServer(flight.FlightID, server.Id));
                        serversUpdates.Add(serverUpdate);
                    }
                    externalFlights.Add(externals);
                }

                IList<Flight.Flight> temp = await localFlights;

                foreach (var flightsList in externalFlights)
                {
                    temp = temp.Concat(await flightsList).ToList();
                }

                foreach (var update in serversUpdates)
                {
                    await update;
                }

                return temp;
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
