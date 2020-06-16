using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightPlan
    {
        private readonly IIDGenerator idGenerator = new IDGenerator("AYR");

        private readonly string id;
        /// <summary>
        /// A getter for the id member of the flightplan.
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
        [Newtonsoft.Json.JsonProperty("passengers")]
        [JsonPropertyName("passengers")]
        public int Passengers
        {
            get => passengers;
            set => passengers = value;
        }


        private string company;
        /// <summary>
        /// The company managing the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("company_name")]
        [JsonPropertyName("company_name")]
        public string Company
        {
            get => company;
            set => company = value;
        }


        private InitialLocation initLocation;
        /// <summary>
        /// The initial location of the flight.
        /// Where the flight takes off.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("initial_location")]
        [JsonPropertyName("initial_location")]
        public InitialLocation InitLocation
        {
            get => initLocation;
            set => initLocation = value;
        }


        private IList<FlightStatus> segments;
        /// <summary>
        /// A list of locations where the flight is passing.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("segments")]
        [JsonPropertyName("segments")]
        public IList<FlightStatus> Segments
        {
            get => segments;
            set => segments = value;
        }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="passengers"> The number of passengers on the flight. </param>
        /// <param name="company"> The name of the company managing the flight. </param>
        /// <param name="initialLocation"> The initial location / where the flight
        /// takes off. </param>
        /// <param name="segments"> A list of locations where the flight is passing. </param>
        [JsonConstructor]
        public FlightPlan(int passengers, string company, InitialLocation initialLocation,
            IList<FlightStatus> segments)
        {
            this.id = idGenerator.GenerateID();
            Passengers = passengers;
            Company = company;
            InitLocation = initialLocation;
            Segments = segments;
        }


        /// <summary>
        /// Create a flight plan using the database representation of a flight plan.
        /// </summary>
        /// <param name="fp"> The database representation of a flight plan. </param>
        public FlightPlan(FlightPlanDBRep dataBaseRepresentation)
        {
            Passengers = dataBaseRepresentation.Passengers;
            id = dataBaseRepresentation.GetID();
            Company = dataBaseRepresentation.Company;
            InitLocation = JsonConvert.DeserializeObject<InitialLocation>(
                dataBaseRepresentation.InitLocation
                );
            Segments = JsonConvert.DeserializeObject<IList<FlightStatus>>(dataBaseRepresentation.Segments);
        }


        /// <summary>
        /// A default constructor.
        /// </summary>
        public FlightPlan()
        {
            this.id = idGenerator.GenerateID();
        }
    }
}
