using clientManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;


namespace clientManagement.Data
{
    public class ClientRepository
    {
        private readonly string _connectionString= "Data Source=TO-20S8QY3;Initial Catalog=ClientManagementDB;User ID=sa;Password=1234;Encrypt=True;Trust Server Certificate=True";
       
        public IEnumerable<Client> ListClients()
        {
            var clients = new List<Client>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("ListClients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    { 
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                CPF = (string)reader["CPF"],
                                ClientTypeId = (int)reader["ClientTYpeId"],
                                ClientStatusId = (int)reader["ClientStatusId"]
                            });
                        }
                    }
                }
            }

            return clients;
        }

        public Client GetClientById(int id)
        {
            Client client = null;

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("GetClientById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                CPF = (string)reader["CPF"],
                                ClientTypeId = (int)reader["ClientTYpeId"],
                                ClientStatusId = (int)reader["ClientStatusId"]
                            };
                        }
                    }
                }
            }

            return client;
        }

        public void CreateClient(Client client)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("CreateClient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", client.Name);
                    cmd.Parameters.AddWithValue("@CPF", client.CPF);
                    cmd.Parameters.AddWithValue("@ClientTypeId", client.ClientTypeId);
                    cmd.Parameters.AddWithValue("@ClientstatusId", client.ClientStatusId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateClient(Client client)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("UpdateClient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", client.Id);
                    cmd.Parameters.AddWithValue("@Name", client.Name);
                    cmd.Parameters.AddWithValue("@CPF", client.CPF);
                    cmd.Parameters.AddWithValue("@ClientTypeId", client.ClientTypeId);
                    cmd.Parameters.AddWithValue("@ClientStatusId", client.ClientStatusId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteClient(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("DeleteClient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ClientAlreadyExist(string cpf, int? id = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM Cliente WHERE CPF = @CPF AND (@Id IS NULL OR Id <> @Id)", conn))
                {
                    cmd.Parameters.AddWithValue("@CPF", cpf);
                    cmd.Parameters.AddWithValue("@Id", id.HasValue ? (object)id.Value : DBNull.Value);
                    conn.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }
    }
}
