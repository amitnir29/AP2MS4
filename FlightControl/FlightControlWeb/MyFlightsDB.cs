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


namespace FlightControlWeb
{
    public class MyFlightsDB : IFlightsDB
    {
        private readonly string connectionString;
        //private int rows;

        public MyFlightsDB(IConfiguration configuration)
        {
            connectionString = configuration["DefConnectionString"];
        }

        public async Task DeleteFlightPlan(string id)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("DELETE FROM FlightPlans WHERE id = '" + id + "'", con);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<FlightPlan> GetFlightPlan(string id)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT * FROM FlightPlans WHERE id = '" + id + "'", con);
            try
            {
                using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
                await rdr.ReadAsync();
                string tryid = rdr.GetString(1);
                int pass = rdr.GetInt32(2);
                string comp = rdr.GetString(3);
                string init = rdr.GetString(4);
                string segs = rdr.GetString(5);
                FlightPlanDBRep fpdb = new FlightPlanDBRep(tryid, pass, comp, init, segs);
                return new FlightPlan(fpdb);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async IAsyncEnumerable<FlightPlan> GetAllFlightPlans()
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT * FROM FlightPlans", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                string tryid = rdr.GetString(1);
                int pass = rdr.GetInt32(2);
                string comp = rdr.GetString(3);
                string init = rdr.GetString(4);
                string segs = rdr.GetString(5);

                FlightPlanDBRep fpdb = new FlightPlanDBRep(tryid, pass, comp, init, segs);
                FlightPlan fp = new FlightPlan(fpdb);
                yield return fp;
            }
            yield break;
        }

        public async Task AddFlightPlan(FlightPlan flightPlan)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("INSERT into FlightPlans (id, Passengers, Company, InitLocation, Segments) " +
                    "VALUES (@Id, @Passengers, @Company, @InitLocation, @Segments)", con);
            FlightPlanDBRep fpdb = new FlightPlanDBRep(flightPlan);
            command.Parameters.AddWithValue("@Id", fpdb.GetID());
            command.Parameters.AddWithValue("@Passengers", fpdb.Passengers);
            command.Parameters.AddWithValue("@Company", fpdb.Company);
            command.Parameters.AddWithValue("@InitLocation", fpdb.InitLocation);
            command.Parameters.AddWithValue("@Segments", fpdb.Segments);
            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                //TODO decide what to do if there is this id already
            }
        }

        private async Task<int> NumOfRows()
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT COUNT(*) FROM FlightPlans", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            await rdr.ReadAsync();
            return rdr.GetInt32(0);
        }
    }
}
