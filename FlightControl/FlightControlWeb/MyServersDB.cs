﻿using FlightControlWeb.DB;
using FlightControlWeb.Servers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb
{
    public class MyServersDB : IServersDB
    {
        private readonly string connectionString;

        public MyServersDB(IConfiguration configuration)
        {
            connectionString = configuration["DefConnectionString"];
        }

        public async Task DeleteServer(string id)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("DELETE FROM Servers WHERE id = '" + id + "'", con);
            await command.ExecuteNonQueryAsync();
        }

        public async IAsyncEnumerable<Server> GetAllServers()
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT * FROM Servers", con);
            using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                string tryid = rdr.GetString(0);
                string url = rdr.GetString(1);

                Server server = new Server(tryid, url);
                yield return server;
            }
            yield break;
        }

        public async Task<Server> GetServer(string id)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("SELECT * FROM Servers WHERE id = '" + id + "'", con);
            try
            {
                using SQLiteDataReader rdr = (SQLiteDataReader)await command.ExecuteReaderAsync();
                await rdr.ReadAsync();
                string tryid = rdr.GetString(0);
                string url = rdr.GetString(1);
                return new Server(tryid, url);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task AddServer(Server server)
        {
            using SQLiteConnection con = new SQLiteConnection(connectionString);
            await con.OpenAsync();
            using var command = new SQLiteCommand("INSERT into Servers (id, url) VALUES (@Id, @Url)", con);
            command.Parameters.AddWithValue("@Id", server.Id);
            command.Parameters.AddWithValue("@Url", server.Url);
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
