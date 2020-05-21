using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightPlane
    {
        private int passengers;
        [JsonPropertyName("passengers")]
        public int Passengers
        {
            get => passengers;
            set => passengers = value;
        }


        private string company;
        [JsonPropertyName("company_name")]
        public string Company
        {
            get => company;
            set => company = value;
        }


        private InitialLocation initLocation;
        [JsonPropertyName("initial_location")]
        public InitialLocation InitLocation
        {
            get => initLocation;
            set => initLocation = value;
        }


        private IList<FlightStatus> segments;
        [JsonPropertyName("segments")]
        public IList<FlightStatus> Segments
        {
            get => segments;
            set => segments = value;
        }


        public FlightPlane(int passengers, string company, InitialLocation initialLocation, IList<FlightStatus> segments)
        {
            Passengers = passengers;
            Company = company;
            InitLocation = initialLocation;
            Segments = segments;
        }
    }
}
