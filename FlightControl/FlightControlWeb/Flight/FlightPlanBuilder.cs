using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightPlanBuilder : IFlightPlanBuilder
    {
        // A generator to generate an id for the flight plan.
        private IIDGenerator idGenerator = new IDGenerator("AYR");

        private string id;


        private int passengers;
        /// <summary>
        /// The bunberof passengers on the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("passengers")]
        [JsonPropertyName("passengers")]
        public int Passengers
        {
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
            set => company = value;
        }


        private InitialLocationBuilder initLocationBuilder;
        /// <summary>
        /// The location from which the flight is taking off.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("initial_location")]
        [JsonPropertyName("initial_location")]
        public InitialLocationBuilder InitLocationBuilder
        {
            set => initLocationBuilder = value;
        }


        private IList<FlightStatusBuilder> segmentsBuilders;
        /// <summary>
        /// Locations where the flight is passing.
        /// </summary>
        [JsonProperty("segments")]
        [JsonPropertyName("segments")]
        public IList<FlightStatusBuilder> Segments
        {
            set => segmentsBuilders = value;
        }


        /// <summary>
        /// Create a flight plan based on the builder.
        /// </summary>
        /// <returns> A flight plan based on the builder. </returns>
        public FlightPlan Create()
        {
            InitialLocation initialLocation = this.initLocationBuilder.Create();

            IList<FlightStatus> segments = new List<FlightStatus>();

            foreach (IFlightStatusBuilder flightStatusBuilder in this.segmentsBuilders)
            {
                FlightStatus segment = flightStatusBuilder.Create();
                segments.Add(segment);
            }

            return new FlightPlan(this.id, this.passengers, this.company, initialLocation, segments);
        }


        public FlightPlanBuilder()
        {
            this.id = idGenerator.GenerateID();
        }
    }
}
