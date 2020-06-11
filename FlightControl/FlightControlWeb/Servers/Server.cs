using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Servers
{
    public class Server
    {
        private string id;
        [JsonPropertyName("ServerId")]
        public string Id
        {
            get => id;
            set => id = value;
        }


        private string url;
        [JsonPropertyName("ServerURL")]
        public string Url
        {
            get => url;
            set => url = value;
        }


        public Server(string id, string url)
        {
            Id = id;
            Url = url;
        }


        public Server() { }
    }
}
