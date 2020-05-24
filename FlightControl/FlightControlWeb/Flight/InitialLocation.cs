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
        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get => longitude;
            set => longitude = value;
        }

        private double latitude;
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get => latitude;
            set => latitude = value;
        }


        private string time;
        [JsonPropertyName("date_time")]
        public string Time
        {
            get => time;
            set => time = value;
        }


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
    }
}
