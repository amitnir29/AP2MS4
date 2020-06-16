using FlightControlWeb.DB;
using FlightControlWeb.Servers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    public class MyFlightsServersDB : IFlightsServersDB
    {
        private readonly string connectionString;

        public MyFlightsServersDB(IConfiguration configuration)
        {
            // The connection string to the DB
            connectionString = configuration["DefConnectionString"];
        }

        public async Task DeleteServer(string serverid)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // Delete from DB
            using var command = new SQLiteCommand("DELETE FROM FlightsServers WHERE serverid = '" +
                serverid + "'", con);
            var res = await command.ExecuteNonQueryAsync();

            // Failed
            if (res == 0)
                throw new ArgumentException("No server with id " + serverid);
        }

        public async Task<FlightServer> GetFlightServer(string flightid)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // Get that flightserver from DB
            using var command = new SQLiteCommand(
                "SELECT * FROM FlightsServers WHERE flightid = '" + flightid + "'", con);
            try
            {
                // Reading it
                using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
                await rdr.ReadAsync();
                string fid = rdr.GetString(0);
                string sid = rdr.GetString(1);
                // Returning it
                return new FlightServer(fid, sid);
            }
            // Failed
            catch (Exception)
            {
                return null;
            }
        }

        public async IAsyncEnumerable<FlightServer> GetServerIterator(string serverid)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // Get all the flightservers with that serverid
            using var command = new SQLiteCommand("SELECT * FROM FlightsServers WHERE serverid = '"
                + serverid + "'", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            // Reading and returning them one by one
            while (await rdr.ReadAsync())
            {
                // Getting
                string fid = rdr.GetString(0);
                string sid = rdr.GetString(1);

                // Returning
                FlightServer fs = new FlightServer(fid, sid);
                yield return fs;
            }
            yield break;
        }

        public async Task AddFlightServer(FlightServer fs)
        {
            FlightServer exists = await this.GetFlightServer(fs.FlightId);

            if (exists != null)
                return;

            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();

            // Creating the query to insert
            using var command = new SQLiteCommand(
                "INSERT into FlightsServers (flightid, serverid) VALUES (@FId, @SId)", con);
            // Inserting the parameters with value
            command.Parameters.AddWithValue("@FId", fs.FlightId);
            command.Parameters.AddWithValue("@SId", fs.ServerId);
            try
            {
                // Writing that to DB
                var res = await command.ExecuteNonQueryAsync();

                // Failed
                if (res == 0)
                    throw new ArgumentException("Failed post serverflight.");
            }
            // The id is already there so failed
            catch (Exception)
            {
                throw new ArgumentException("Serverflight already in data base.");
            }
        }
    }
}
