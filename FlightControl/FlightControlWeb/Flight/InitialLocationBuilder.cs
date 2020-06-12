using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class InitialLocationBuilder : IInitialLocationBuilder
    {
        private double longitude;
        /// <summary>
        /// Longitude position of the initial location.
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
        /// Latitude position of the initial location.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            set => latitude = value;
        }


        private string time;
        /// <summary>
        /// The time (UTC) in which the plane is at the initial location.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("date_time")]
        [JsonPropertyName("date_time")]
        public string Time
        {
            set => time = value;
        }


        /// <summary>
        /// Create an initial location based on the builder.
        /// </summary>
        /// <returns> An initial location based on the builder. </returns>
        public InitialLocation Create()
        {
            return new InitialLocation(longitude, latitude, DateTime.Parse(time));
        }
    }
}
