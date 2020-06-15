using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightPlanDBRep
    {
        private readonly string id;
        /// <summary>
        /// A getter for the id of the flight plan.
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            return id;
        }


        private int passengers;
        /// <summary>
        /// The number of passengers on the flight.
        /// </summary>
        public int Passengers
        {
            get => passengers;
            set => passengers = value;
        }


        private string company;
        /// <summary>
        /// The company which manages the flight.
        /// </summary>
        public string Company
        {
            get => company;
            set => company = value;
        }


        private string initLocation;
        /// <summary>
        /// A json representation of the initial location of the flight.
        /// </summary>
        public string InitLocation
        {
            get => initLocation;
            set => initLocation = value;
        }


        private string segments;
        /// <summary>
        /// A json representation of the segments of the flight.
        /// </summary>
        public string Segments
        {
            get => segments;
            set => segments = value;
        }


        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="id"> The id of the flight. </param>
        /// <param name="passengers"> The number of passengers on the flight. </param>
        /// <param name="company"> The company which manages the flight. </param>
        /// <param name="initialLocation"> A json representation of the initial location
        /// of the flight. </param>
        /// <param name="segments"> A json representation of the segments of the flight. </param>
        public FlightPlanDBRep(string id, int passengers, string company, string initialLocation,
            string segments)
        {
            this.id = id;
            Passengers = passengers;
            Company = company;
            InitLocation = initialLocation;
            Segments = segments;
        }


        /// <summary>
        /// Create a database representation of a flight plan base on a flight plan object.
        /// </summary>
        /// <param name="fp"> The flight plan to create the database representation from. </param>
        public FlightPlanDBRep(FlightPlan fp)
        {
            id = fp.GetID();
            Passengers = fp.Passengers;
            Company = fp.Company;
            InitLocation = System.Text.Json.JsonSerializer.Serialize(fp.InitLocation);
            Segments = System.Text.Json.JsonSerializer.Serialize(fp.Segments);
        }
    }
}
