using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Flight
{
    public class FlightPlanDB
    {
        private string id;
        public string GetID()
        {
            return id;
        }


        private int passengers;
        public int Passengers
        {
            get => passengers;
            set => passengers = value;
        }


        private string company;
        public string Company
        {
            get => company;
            set => company = value;
        }


        private string initLocation;
        public string InitLocation
        {
            get => initLocation;
            set => initLocation = value;
        }


        private string segments;
        public string Segments
        {
            get => segments;
            set => segments = value;
        }


        public FlightPlanDB(string id, int passengers, string company, string initialLocation, string segments)
        {
            this.id = id;
            Passengers = passengers;
            Company = company;
            InitLocation = initialLocation;
            Segments = segments;
        }

        public FlightPlanDB(FlightPlan fp)
        {
            id = fp.GetID();
            Passengers = fp.Passengers;
            Company = fp.Company;
            InitLocation = System.Text.Json.JsonSerializer.Serialize(fp.InitLocation);
            Segments = System.Text.Json.JsonSerializer.Serialize(fp.Segments);
        }
    }
}
