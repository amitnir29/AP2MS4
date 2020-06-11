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
                // Try get the flight plan from the local data base.
                var local = await flightsDB.GetFlightPlan(id);

                if (local != null)
                    return local;

                // If flight plan not in local data base, try to retrieve it from an external server.
                // Get the server in which the flight plan is located.
                var relevantServer = await flightsServersDB.GetFlightServer(id);

                if (relevantServer != null) {
                    // Ask the external server for the flight plan.
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

                /* Create a list contains lists of external flights.
                 * Each inner list represents flights from a specifiec server. */
                IList<Task<IList<Flight.Flight>>> externalFlights = new List<Task<IList<Flight.Flight>>>();

                IList<KeyValuePair<string, string>> serversUpdates = new List<KeyValuePair<string, string>>();

                await foreach (Server server in serversDB.GetIterator())
                {
                    // For each server, ask it for all its flights.
                    HTTPClient client = new HTTPClient(server);
                    var externals = client.GetFlights(relativeTo.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                    var res = await externals;

                    if (res == null)
                        continue;

                    foreach(var flight in res)
                    {
                        // Change isExternal property of the flight to true, as it was retrieved from an external server.
                        flight.IsExternal = true;
                        serversUpdates.Add(new KeyValuePair<string, string>(flight.FlightID, server.Id));
                    }
                        externalFlights.Add(externals);
                    }

                    IList<Flight.Flight> temp = await localFlights;

                // Join all the lists from the serers and the list of the local flight to one list of flights.
                foreach (var flightsList in externalFlights)
                {
                    temp = temp.Concat(await flightsList).ToList();
                }

                foreach (var pair in serversUpdates)
                {
                /* Add the id of the flight to the data base. Connect it to its server. Next time we want the flight plan we can
                * simply ask it from the relevant server. */
                await flightsServersDB.PostFlightServer(new Servers.FlightServer(pair.Key, pair.Value));
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