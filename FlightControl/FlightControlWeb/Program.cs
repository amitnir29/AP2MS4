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
            //CreateHostBuilder(args).Build().Run();
            MyFlightsDB db = new MyFlightsDB("Data Source=.\\FlightPlansDB.db;Version=3;");
            //FlightPlan fp = new FlightPlan("AYRTRY2", 2185, "AYR", null, null);
            //db.GetFlightPlan("AYRTRY2");
            db.GetIterator();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
