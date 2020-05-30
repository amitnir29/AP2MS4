using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using FlightControlWeb.Flight;
using System.Threading;
using FlightControlWeb.Servers;

namespace FlightControlWeb
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine(args);
            CreateHostBuilder(args).Build().Run();
            //MyFlightsServersDB flightsServers = new MyFlightsServersDB("Data Source=.\\FlightsServersDB.db;Version=3;");
            //flightsServers.PostFlightServer(new FlightServer("AYRTRY", "SERTRY"));
            //flightsServers.PostFlightServer(new FlightServer("AYRTRY2", "SERTRY"));
            //flightsServers.PostFlightServer(new FlightServer("AYRTRY3", "SERTRY1"));
            //flightsServers.DeleteServer("SERTRY");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
