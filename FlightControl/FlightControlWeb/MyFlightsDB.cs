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

namespace FlightControlWeb
{
    public class MyFlightsDB : IFlightsDB
    {
        private readonly string connectionString;

        public MyFlightsDB(string con)
        {
            connectionString = con;
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
            using SQLiteDataReader rdr = (SQLiteDataReader) await command.ExecuteReaderAsync();
            await rdr.ReadAsync();
            string tryid = rdr.GetString(1);
            int pass = rdr.GetInt32(2);
            string comp = rdr.GetString(3);
            string init = rdr.GetString(4);
            string segs = rdr.GetString(5);
            FlightPlanDB fpdb = new FlightPlanDB(tryid, pass, comp, init, segs);
            return new FlightPlan(fpdb);
        }

        public async IAsyncEnumerable<FlightPlan> GetIterator()
        {
            int rows = await NumOfRows();
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

                FlightPlanDB fpdb = new FlightPlanDB(tryid, pass, comp, init, segs);
                FlightPlan fp = new FlightPlan(fpdb);
                yield return fp;
            }
        }

        public async Task PostFlightPlan(FlightPlan flightPlan)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("INSERT into FlightPlans (id, Passengers, Company, InitLocation, Segments) " +
                    "VALUES (@Id, @Passengers, @Company, @InitLocation, @Segments)", con);
            FlightPlanDB fpdb = new FlightPlanDB(flightPlan);
            command.Parameters.AddWithValue("@Id", fpdb.GetID());
            command.Parameters.AddWithValue("@Passengers", fpdb.Passengers);
            command.Parameters.AddWithValue("@Company", fpdb.Company);
            command.Parameters.AddWithValue("@InitLocation", fpdb.InitLocation);
            command.Parameters.AddWithValue("@Segments", fpdb.Segments);
            await command.ExecuteNonQueryAsync();
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
