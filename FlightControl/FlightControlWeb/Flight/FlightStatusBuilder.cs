using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightStatusBuilder : IFlightStatusBuilder
    {
        private double longitude;
        /// <summary>
        /// Longitude position of the flight in the current status.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public double Longitude
        {
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
        /// Latitude position of the flight in the current status.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            set => latitude = value;
        }


        private int deltaTime;
        /// <summary>
        /// THe difference (delta) of time from the previous segment of the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("timespan_seconds")]
        [JsonPropertyName("timespan_seconds")]
        public int DeltaTime
        {
            set => deltaTime = value;
        }


        /// <summary>
        /// Create a flight status based on the builder.
        /// </summary>
        /// <returns> A flight status based on the builder. </returns>
        public FlightStatus Create()
        {
            return new FlightStatus(longitude, latitude, deltaTime);
        }
    }
}
