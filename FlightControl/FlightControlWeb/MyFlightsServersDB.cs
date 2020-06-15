using FlightControlWeb.DB;
using FlightControlWeb.Servers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace FlightControlWeb
{
    public class MyFlightsServersDB : IFlightsServersDB
    {
        private readonly string connectionString;

        public MyFlightsServersDB(IConfiguration configuration)
        {
            connectionString = configuration["DefConnectionString"];
        }

        public async Task DeleteServer(string serverid)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("DELETE FROM FlightsServers WHERE serverid = '" + serverid + "'", con);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<FlightServer> GetFlightServer(string flightid)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT * FROM FlightsServers WHERE flightid = '" + flightid + "'", con);
            try
            {
                using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
                await rdr.ReadAsync();
                string fid = rdr.GetString(0);
                string sid = rdr.GetString(1);
                return new FlightServer(fid, sid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async IAsyncEnumerable<FlightServer> GetServerIterator(string serverid)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT * FROM FlightsServers WHERE serverid = '" + serverid + "'", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                string fid = rdr.GetString(0);
                string sid = rdr.GetString(1);

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

            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();

            using var command = new SQLiteCommand("INSERT into FlightsServers (flightid, serverid) VALUES (@FId, @SId)", con);
            command.Parameters.AddWithValue("@FId", fs.FlightId);
            command.Parameters.AddWithValue("@SId", fs.ServerId);
            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                //TODO decide what to do if there is this id already
            }
        }
    }
}
