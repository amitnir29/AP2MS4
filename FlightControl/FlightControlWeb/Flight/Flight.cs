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
        [Newtonsoft.Json.JsonProperty("flight_id")]
        [JsonPropertyName("flight_id")]
        public string FlightID
        {
            get => flightID;
            set => flightID = value;
        }


        private int passengers;
        [Newtonsoft.Json.JsonProperty("passengers")]
        [JsonPropertyName("passengers")]
        public int Passengers
        {
            get => passengers;
            set => passengers = value;
        }

        private string company;
        [Newtonsoft.Json.JsonProperty("company_name")]
        [JsonPropertyName("company_name")]
        public string Company
        {
            get => company;
            set => company = value;
        }

        private bool isExternal;
        [Newtonsoft.Json.JsonProperty("is_external")]
        [JsonPropertyName("is_external")]
        public bool IsExternal
        {
            get => isExternal;
            set => isExternal = value;
        }


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


        private string time;

        [Newtonsoft.Json.JsonProperty("date_time")]
        [JsonPropertyName("date_time")]
        public string Time
        {
            get => time;
            set => time = value;
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
        /// <param name="isExternal"> Indicator if the flight is given from external server or not. </param>
        public Flight(string flightID, double longitude, double latitude, int passengers, string company, DateTime time, bool isExternal)
        {
            FlightID = flightID;
            Passengers = passengers;
            Company = company;
            IsExternal = isExternal;
            Longitude = longitude;
            Latitude = latitude;
            Time = time.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }


        public Flight() { }

    }
}
