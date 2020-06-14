using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class InitialLocation
    {
        private double longitude;
        /// <summary>
        /// Longitude position of the flight in the current status.
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
        /// Latitude position of the flight in the current status.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get => latitude;
            set => latitude = value;
        }


        private string time;
        /// <summary>
        /// The time in which the flight is taking off.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("date_time")]
        [JsonPropertyName("date_time")]
        public string Time
        {
            get => time;
            set => time = value;
        }


        /// <summary>
        /// A default constructor.
        /// </summary>
        public InitialLocation(){ }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="longitude"> Longitude of the plane at the current status. </param>
        /// <param name="latitude"> Latitude of the plane at the current status. </param>
        /// <param name="time"> Time at the current status. </param>
        public InitialLocation(double longitude, double latitude, DateTime time)
        {
            Longitude = longitude;
            Latitude = latitude;
            Time = time.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="longitude"> Longitude of the plane at the current status. </param>
        /// <param name="latitude"> Latitude of the plane at the current status. </param>
        /// <param name="time"> Time at the current status. </param>
        [JsonConstructor]
        public InitialLocation(double longitude, double latitude, string time)
        {
            Longitude = longitude;
            Latitude = latitude;
            Time = time;
        }
    }
}
