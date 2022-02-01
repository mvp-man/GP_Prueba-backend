using Models;
using Repository.Interfaces;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.SqlServer
{
    public class ClientRepository : Repository, IClientRepository
    {
        public ClientRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }       
        public Client Get(int id)
        {
            var result = new Client();
            var command = CreateCommand("select * from Clients with(nolock) where id= @clientId");
            command.Parameters.AddWithValue("@clientId", id);
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                return new Client
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["name"].ToString()
                };
            }
        }

        public IEnumerable<Client> GetAll()
        {
            var result = new List<Client>();
            var command = CreateCommand("select * from Clients with(nolock)");            
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Client
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["name"].ToString()
                    });
                }
                return result;
            }
        }
    }
}
