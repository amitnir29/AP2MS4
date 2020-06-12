using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightPlan
    {
        private string id;
        /// <summary>
        /// A getter for the id of the flight.
        /// </summary>
        /// <returns> The id of the flight represented by the flight plan. </returns>
        public string GetID()
        {
            return id;
        }


        private int passengers;
        /// <summary>
        /// The bunberof passengers on the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("passengers")]
        [JsonPropertyName("passengers")]
        public int Passengers
        {
            get => passengers;
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
        }


        private InitialLocation initLocation;
        /// <summary>
        /// The location from which the flight is taking off.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("initial_location")]
        [JsonPropertyName("initial_location")]
        public InitialLocation InitLocation
        {
            get => initLocation;
        }


        private IList<FlightStatus> segments;
        /// <summary>
        /// Locations where the flight is passing.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("segments")]
        [JsonPropertyName("segments")]
        public IList<FlightStatus> Segments
        {
            get => segments;
        }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="id"> The id of the flight. </param>
        /// <param name="passengers"> Number of passengers on the flight. </param>
        /// <param name="company"> The company managing the flight. </param>
        /// <param name="initialLocation"> The location from which the flight is taking off. </param>
        /// <param name="segments"> Locations where the flight is passing. </param>
        public FlightPlan(string id, int passengers, string company, InitialLocation initialLocation, IList<FlightStatus> segments)
        {
            this.id = id;
            this.passengers = passengers;
            this.company = company;
            this.initLocation = initialLocation;
            this.segments = segments;
        }


        public FlightPlan(FlightPlanDB fp)
        {
            this.passengers = fp.Passengers;
            id = fp.GetID();
            this.company = fp.Company;

            IInitialLocationBuilder initialLocationBuilder = JsonSerializer.Deserialize<InitialLocationBuilder>(fp.InitLocation);
            this.initLocation = initialLocationBuilder.Create();

            IList<FlightStatusBuilder> flightStatusBuilders = JsonSerializer.Deserialize<IList<FlightStatusBuilder>>(fp.Segments);
            this.segments = new List<FlightStatus>();

            foreach (FlightStatusBuilder flightStatusBuilder in flightStatusBuilders)
            {
                FlightStatus status = flightStatusBuilder.Create();
                Segments.Add(status);
            }
        }
    }
}
