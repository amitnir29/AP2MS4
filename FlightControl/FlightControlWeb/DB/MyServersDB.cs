using FlightControlWeb.DB;
using FlightControlWeb.Servers;
using Microsoft.CodeAnalysis;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DB
{
    public class MyServersDB : IServersDB
    {
        private readonly string connectionString;

        public MyServersDB(IConfiguration configuration)
        {
            // The connection string to the DB
            connectionString = configuration["DefConnectionString"];
        }

        public async Task DeleteServer(string id)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();

            // Delete the server from the DB
            using var command = new SQLiteCommand("DELETE FROM Servers WHERE id = '"
                + id + "'", con);
            var res = await command.ExecuteNonQueryAsync();

            // If the server wasn't in the DB
            if (res == 0)
                throw new ArgumentException("No server with id " + id);
        }

        public async IAsyncEnumerable<Server> GetAllServers()
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();

            // Get all servers from DB
            using var command = new SQLiteCommand("SELECT * FROM Servers", con);

            // Read the first one
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            // Read and return one by one
            while (await rdr.ReadAsync())
            {
                // Creating the new object
                string tryid = rdr.GetString(0);
                string url = rdr.GetString(1);
                Server server = new Server(tryid, url);
                // Returning it
                yield return server;
            }
            yield break;
        }

        public async Task<Server> GetServer(string id)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();

            // Get the server from the DB
            using var command = new SQLiteCommand("SELECT * FROM Servers WHERE id = '"
                + id + "'", con);
            try
            {
                // Executing the command
                using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
                await rdr.ReadAsync();
                // Creating the new object
                string tryid = rdr.GetString(0);
                string url = rdr.GetString(1);
                return new Server(tryid, url);
            }
            // Something happened
            catch (Exception)
            {
                return null;
            }
        }

        public async Task AddServer(Server server)
        {
            // Opening the connection
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            // The insert command
            using var command = new SQLiteCommand(
                "INSERT into Servers (id, url) VALUES (@Id, @Url)", con);
            // Insert th parameters
            command.Parameters.AddWithValue("@Id", server.Id);
            command.Parameters.AddWithValue("@Url", server.Url);
            try
            {
                // Execute
                var res = await command.ExecuteNonQueryAsync();

                // Failed
                if (res == 0)
                    throw new ArgumentException("No server with id " + server.Id);
            }
            // The id was already there
            catch (Exception)
            {
                throw new ArgumentException("Server id already in database.");
            }
        }
    }
}
