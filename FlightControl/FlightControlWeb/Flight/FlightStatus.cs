﻿using System;
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
        /// Longitude position of the flight in the current status.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get => longitude;
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
        }


        private int deltaTime;
        /// <summary>
        /// THe difference (delta) of time from the previous segment of the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("timespan_seconds")]
        [JsonPropertyName("timespan_seconds")]
        public int DeltaTime
        {
            get => deltaTime;
        }


        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="longitude"> Longitude of the plane at the current status. </param>
        /// <param name="latitude"> Latitude of the plane at the current status. </param>
        /// <param name="deltaTime"> Time passed from last status. </param>
        public FlightStatus(double longitude, double latitude, int deltaTime)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.deltaTime = deltaTime;
        }
    }
}