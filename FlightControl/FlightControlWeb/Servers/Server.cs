using Newtonsoft.Json;
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
        /// <summary>
        /// The id of the server.
        /// </summary>
        [JsonPropertyName("ServerId")]
        public string Id
        {
            get => id;
            set => id = value;
        }


        private string url;
        /// <summary>
        /// The url of the server.
        /// </summary>
        [JsonPropertyName("ServerURL")]
        public string Url
        {
            get => url;
            set => url = value;
        }


        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="id"> The id of the server. </param>
        /// <param name="url"> The url of the server. </param>
        [JsonConstructor]
        public Server(string id, string url)
        {
            Id = id;
            Url = url;
        }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public Server()
        {
        }
    }
}
