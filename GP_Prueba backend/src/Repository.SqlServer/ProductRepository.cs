using Models;
using Repository.Interfaces;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.SqlServer
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }       
        public Product Get(int id)
        {
            var result = new Product();
            var command = CreateCommand("select * from Products with(nolock) where id= @productId");
            command.Parameters.AddWithValue("@productId", id);
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                return new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"])
                };
            }
        }
        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
