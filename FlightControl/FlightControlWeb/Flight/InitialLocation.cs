﻿using System;
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
        /// Longitude position of the initial location.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get => longitude;
        }


        private double latitude;
        /// <summary>
        /// Latitude position of the initial location.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get => latitude;
        }


        private string time;
        /// <summary>
        /// The time (UTC) in which the plane is at the initial location.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("date_time")]
        [JsonPropertyName("date_time")]
        public string Time
        {
            get => time;
        }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="longitude"> Longitude of the plane at the current status. </param>
        /// <param name="latitude"> Latitude of the plane at the current status. </param>
        /// <param name="time"> Time at the current status. </param>
        public InitialLocation(double longitude, double latitude, DateTime time)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.time = time.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
