using FlightControlWeb.DB;
using FlightControlWeb.Servers;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlWeb
{
    public class MyFlightsServersDB : IFlightsServersDB
    {
        private readonly string connectionString;

        public MyFlightsServersDB(string con)
        {
            connectionString = con;
        }

        public async Task DeleteServer(string serverid)
        {
            using SqliteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("DELETE FROM FlightsServers WHERE serverid = '" + serverid + "'", con);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<FlightServer> GetFlightServer(string flightid)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT * FROM FlightsServers WHERE flightid = '" + flightid + "'", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            await rdr.ReadAsync();
            string fid = rdr.GetString(0);
            string sid = rdr.GetString(1);
            return new FlightServer(fid, sid);
        }

        public async IAsyncEnumerable<FlightServer> GetServerIterator(string serverid)
        {
            int rows = await NumOfRows();
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
        }

        public async Task PostFlightServer(FlightServer fs)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("INSERT into FlightsServers (flightid, serverid) VALUES (@FId, @SId)", con);
            command.Parameters.AddWithValue("@FId", fs.FlightId);
            command.Parameters.AddWithValue("@SId", fs.ServerId);
            await command.ExecuteNonQueryAsync();
        }

        private async Task<int> NumOfRows()
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT COUNT(*) FROM FlightsServers", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            await rdr.ReadAsync();
            return rdr.GetInt32(0);
        }
    }
}
