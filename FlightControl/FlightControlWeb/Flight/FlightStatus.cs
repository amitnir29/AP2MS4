using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightStatus
    {
        private double longitude;
        [Newtonsoft.Json.JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get => longitude;
            set => longitude = value;
        }

        private double latitude;
        [Newtonsoft.Json.JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get => latitude;
            set => latitude = value;
        }


        private int deltaTime;
        [Newtonsoft.Json.JsonProperty("timespan_seconds")]
        [JsonPropertyName("timespan_seconds")]
        public int DeltaTime
        {
            get => deltaTime;
            set => deltaTime = value;
        }


        public FlightStatus() { }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="longitude"> Longitude of the plane at the current status. </param>
        /// <param name="latitude"> Latitude of the plane at the current status. </param>
        /// <param name="deltaTime"> Time passed from last status. </param>
        public FlightStatus(double longitude, double latitude, int deltaTime)
        {
            Longitude = longitude;
            Latitude = latitude;
            DeltaTime = deltaTime;
        }
    }
}