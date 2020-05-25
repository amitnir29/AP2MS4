﻿using FlightControlWeb.DB;
using FlightControlWeb.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model
    {
        public class FlightsHandler /*: IFlightsModel*/
        {

            private IFlightsDB dataBase;
            private IFlightCalculator calculator;

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


            /// <summary>
            /// Get all flights from this server.
            /// </summary>
            /// <param name="relativeTo"> The current time at the user's </param>
            /// <returns> All the flights. </returns>
            public async Task<IList<Flight.Flight>> GetAllFlights(DateTime relativeTo)
            {
                IList<Flight.Flight> flights = new List<Flight.Flight>();

                await foreach (var flightPlan in dataBase.GetIterator())
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
            //public async Task<IList<Flight.Flight>> GetAllFlightsSync(DateTime relativeTo);



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
            public async Task DeleteFlight(string id)
            {
                await dataBase.DeleteFlightPlan(id);
            }
        }
    }
