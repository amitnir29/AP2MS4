using Newtonsoft.Json;
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
        /// <summary>
        /// The longitude position of the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get => longitude;
            set
            {
                if (-180 <= value && value <= 180)
                    longitude = value;
                else
                    throw new ArgumentException("Longitude should be between -180 and 180");
            }
        }


        private double latitude;
        /// <summary>
        /// The latitude position of the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get => latitude;
            set => latitude = value;
        }


        private int deltaTime;
        /// <summary>
        /// The difference of time from the previous segment, in seconds.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("timespan_seconds")]
        [JsonPropertyName("timespan_seconds")]
        public int DeltaTime
        {
            get => deltaTime;
            set => deltaTime = value;
        }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="longitude"> Longitude of the plane at the current status. </param>
        /// <param name="latitude"> Latitude of the plane at the current status. </param>
        /// <param name="deltaTime"> Time passed from last status. </param>
        [JsonConstructor]
        public FlightStatus(double longitude, double latitude, int deltaTime)
        {
            Longitude = longitude;
            Latitude = latitude;
            DeltaTime = deltaTime;
        }


        /// <summary>
        /// A default constructor.
        /// </summary>
        public FlightStatus() { }
    }
}