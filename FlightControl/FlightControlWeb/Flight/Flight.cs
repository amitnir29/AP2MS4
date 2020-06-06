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
        [JsonPropertyName("flight_id")]
        public string FlightID
        {
            get => flightID;
            set => flightID = value;
        }


        private int passengers;
        [JsonPropertyName("passengers")]
        public int Passengers
        {
            get => passengers;
            set => passengers = value;
        }

        private string company;
        [JsonPropertyName("company_name")]
        public string Company
        {
            get => company;
            set => company = value;
        }

        private bool isExternal;
        [JsonPropertyName("is_external")]
        public bool IsExternal
        {
            get => isExternal;
            set => isExternal = value;
        }


        private InitialLocation status;

        [JsonIgnore]
        public InitialLocation Status
        {
            get => status;
            set => status = value;
        }


        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get => status.Longitude;
        }

        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get => status.Latitude;
        }


        [JsonPropertyName("date_time")]
        public string Time
        {
            get => status.Time;
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

            Status = new InitialLocation(longitude, latitude, time);
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
        public Flight(string flightID, string longitude, string latitude, int passengers, string company, string time, bool isExternal)
        {
            FlightID = flightID;
            Passengers = passengers;
            Company = company;
            IsExternal = isExternal;

            DateTime timeObj = DateTime.ParseExact(time, "yyyy-MM-ddTHH:mm:ssZ", null).ToUniversalTime();

            Status = new InitialLocation(Double.Parse(longitude), Double.Parse(latitude), timeObj);
        }

    }
}
