using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentralisedListObjects;
using System.Data.SqlClient;

namespace CentralisedListData
{
    public class ClientData
    {
        private readonly string _connectionString;
        public ClientData(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Client GetClient(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT id, name, is_dirty FROM client_master WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return new Client
                    {
                        id = reader.GetString(reader.GetOrdinal("id")),
                        name = reader.GetString(reader.GetOrdinal("name")),
                        is_dirty = reader.GetBoolean(reader.GetOrdinal("is_dirty")),
                    };
                }
            }
        }


        public void Update(string id, Client client)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE client_master SET name=@name, is_dirty=1 WHERE id = @id AND name<>@name";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", client.name);
                cmd.ExecuteNonQuery();
            }
        }

        public void ResetDirtyFlag(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE client_master SET is_dirty=0 WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Client> Load()
        {
            List<Client> clients = new List<Client>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT id, name, is_dirty FROM client_master order by name";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        Client c = new Client
                        {
                            id = reader.GetString(reader.GetOrdinal("id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            is_dirty = reader.GetBoolean(reader.GetOrdinal("is_dirty")),
                        };
                        clients.Add(c);
                    }
                }
            }
            return clients;
        }

        public void Insert(Client client)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO client_master VALUES(@id, @name, 1)";
                cmd.Parameters.AddWithValue("@id", client.id);
                cmd.Parameters.AddWithValue("@name", client.name);
                cmd.ExecuteNonQuery();
            }
        }

    }


}
