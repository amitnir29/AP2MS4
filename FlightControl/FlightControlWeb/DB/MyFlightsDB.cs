using Dapper;
using FlightControlWeb.Flight;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.DB;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;


namespace FlightControlWeb.DB
{
    public class MyFlightsDB : IFlightsDB
    {
        private readonly string connectionString;

        public MyFlightsDB(IConfiguration configuration)
        {
            // The connection string to the DB
            connectionString = configuration["DefConnectionString"];
        }

        public async Task DeleteFlightPlan(string id)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // The query to delete the flightplan
            using SQLiteCommand command = new SQLiteCommand("DELETE FROM FlightPlans WHERE id = '"
                + id + "'", con);
            // Execute
            int res = await command.ExecuteNonQueryAsync();

            // Failed
            if (res == 0)
                throw new ArgumentException("No flight with id " + id);
        }

        public async Task<FlightPlan> GetFlightPlan(string id)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // Get the flightplan with that id from DB
            using SQLiteCommand command = new SQLiteCommand(
                "SELECT * FROM FlightPlans WHERE id = '" + id + "'", con);
            try
            {
                // Get the object
                using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
                await rdr.ReadAsync();
                string tryid = rdr.GetString(1);
                int pass = rdr.GetInt32(2);
                string comp = rdr.GetString(3);
                string init = rdr.GetString(4);
                string segs = rdr.GetString(5);
                // Create a new one
                FlightPlanDBRep fpdb = new FlightPlanDBRep(tryid, pass, comp, init, segs);
                // Return it
                return new FlightPlan(fpdb);
            }
            // Failed
            catch (Exception)
            {
                return null;
            }
        }

        public async IAsyncEnumerable<FlightPlan> GetAllFlightPlans()
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // Get all from the DB
            using SQLiteCommand command = new SQLiteCommand("SELECT * FROM FlightPlans", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            // Get and return the flightplans one by one
            while (await rdr.ReadAsync())
            {
                // Get the object by his fields
                string tryid = rdr.GetString(1);
                int pass = rdr.GetInt32(2);
                string comp = rdr.GetString(3);
                string init = rdr.GetString(4);
                string segs = rdr.GetString(5);

                // Create it
                FlightPlanDBRep fpdb = new FlightPlanDBRep(tryid, pass, comp, init, segs);
                FlightPlan fp = new FlightPlan(fpdb);

                // Return it
                yield return fp;
            }
            yield break;
        }

        public async Task AddFlightPlan(FlightPlan flightPlan)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // The query to insert
            using var command = new SQLiteCommand(
                "INSERT into FlightPlans (id, Passengers, Company, InitLocation, Segments) " +
                    "VALUES (@Id, @Passengers, @Company, @InitLocation, @Segments)", con);
            FlightPlanDBRep fpdb = new FlightPlanDBRep(flightPlan);
            // Insert the parameters with value
            command.Parameters.AddWithValue("@Id", fpdb.GetID());
            command.Parameters.AddWithValue("@Passengers", fpdb.Passengers);
            command.Parameters.AddWithValue("@Company", fpdb.Company);
            command.Parameters.AddWithValue("@InitLocation", fpdb.InitLocation);
            command.Parameters.AddWithValue("@Segments", fpdb.Segments);
            try
            {
                // Write to DB
                var res = await command.ExecuteNonQueryAsync();

                // Failed
                if (res == 0)
                    throw new ArgumentException("Can't post flight");
            }
            // The id is already there
            catch (Exception e)
            {
                //throw new ArgumentException("Flight id " + flightPlan.GetID()
                //    + "already found in data base.");
                throw e;
            }
        }
    }
}
