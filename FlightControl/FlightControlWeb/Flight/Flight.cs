﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class Flight
    {
        private string flightID;
        /// <summary>
        /// The id of the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("flight_id")]
        [JsonPropertyName("flight_id")]
        public string FlightID
        {
            get => flightID;
            private set => flightID = value;
        }


        private int passengers;
        /// <summary>
        /// The number of passengers on the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("passengers")]
        [JsonPropertyName("passengers")]
        public int Passengers
        {
            get => passengers;
            private set => passengers = value;
        }

        private string company;
        /// <summary>
        /// The company managing the flight.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("company_name")]
        [JsonPropertyName("company_name")]
        public string Company
        {
            get => company;
            private set => company = value;
        }


        private bool isExternal;
        /// <summary>
        /// Indicate if the flight is retrieved from an external server or is generated by
        /// this server.
        /// Is external property is mutable from outside of the class because this state is
        /// not constant.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("is_external")]
        [JsonPropertyName("is_external")]
        public bool IsExternal
        {
            get => isExternal;
            set => isExternal = value;
        }


        private double longitude;
        /// <summary>
        /// The longitude position of the plane.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get => longitude;
            private set
            {
                if (-180 <= value && value <= 180)
                    longitude = value;
                else
                    throw new ArgumentException("Longitude should be between -180 and 180");
            }
        }


        private double latitude;
        /// <summary>
        /// The latitude position of the plane.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get => latitude;
            private set => latitude = value;
        }


        private string time;
        /// <summary>
        /// The time in which the flight is in this status.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("date_time")]
        [JsonPropertyName("date_time")]
        public string Time
        {
            get => time;
            private set => time = value;
        }


        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="flightID"> The id of the flight. </param>
        /// <param name="longitude"> Longitude of the plane. </param>
        /// <param name="latitude"> Latitude of the plane. </param>
        /// <param name="passengers"> Number of passengers on flight. </param>
        /// <param name="company"> The company of the flight. </param>
        /// <param name="time"> Time relative to UTC </param>
        /// <param name="isExternal"> Indicator if the flight is given from external server
        /// or not. </param>
        public Flight(string flightID, double longitude, double latitude, int passengers,
            string company, DateTime time, bool isExternal)
        {
            FlightID = flightID;
            Passengers = passengers;
            Company = company;
            IsExternal = isExternal;
            Longitude = longitude;
            Latitude = latitude;
            Time = time.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }


        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="flightID"> The id of the flight. </param>
        /// <param name="longitude"> Longitude of the plane. </param>
        /// <param name="latitude"> Latitude of the plane. </param>
        /// <param name="passengers"> Number of passengers on flight. </param>
        /// <param name="company"> The company of the flight. </param>
        /// <param name="time"> Time relative to UTC </param>
        /// <param name="isExternal"> Indicator if the flight is given from
        /// external server or not. </param>
        [JsonConstructor]
        public Flight(string flightID, double longitude, double latitude, int passengers,
            string company, string time, bool isExternal)
        {
            FlightID = flightID;
            Passengers = passengers;
            Company = company;
            IsExternal = isExternal;
            Longitude = longitude;
            Latitude = latitude;
            Time = time;
        }
    }
}
