using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.SqlServer
{
    public class InvoiceRepository : Repository, IInvoiceRepository
    {
        public InvoiceRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }
        public void Create(Invoice model)
        {
            var query = "Insert into Invoices(clientId, Iva, SubTotal, Total) output INSERTED.ID values (@clientId, @Iva, @SubTotal, @Total)";
            var command = CreateCommand(query);
            command.Parameters.AddWithValue("@clientId", model.ClientId);
            command.Parameters.AddWithValue("@Iva", model.Iva);
            command.Parameters.AddWithValue("@SubTotal", model.SubTotal);
            command.Parameters.AddWithValue("@Total", model.Total);

            model.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public Invoice Get(int id)
        {
            var result = new Invoice();
            var command = CreateCommand("select * from Invoices where id = @id");
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                return new Invoice
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Iva = Convert.ToDecimal(reader["Iva"]),
                    SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                    Total = Convert.ToDecimal(reader["Total"]),
                    ClientId = Convert.ToInt32(reader["ClientId"])
                };
            };
        }

        public IEnumerable<Invoice> GetAll()
        {
            var result = new List<Invoice>();
            var command = CreateCommand("select * from Invoices WITH(NOLOCK)");

            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                { 
                    result.Add(new Invoice
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Iva = Convert.ToDecimal(reader["Iva"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                        Total = Convert.ToDecimal(reader["Total"]),
                        ClientId = Convert.ToInt32(reader["ClientId"])
                    });
                }
                return result;
            };
        }

        public void Remove(int id)
        {
                var command = CreateCommand("Delete from Invoices where id = @id");
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
        }

        public void Update(Invoice model)
        {
            var query = "update Invoices set clientId = @clientId, Iva = @Iva, SubTotal=@SubTotal, Total=@Total where id = @id";
            var command = CreateCommand(query);
            command.Parameters.AddWithValue("@clientId", model.ClientId);
            command.Parameters.AddWithValue("@Iva", model.Iva);
            command.Parameters.AddWithValue("@SubTotal", model.SubTotal);
            command.Parameters.AddWithValue("@Total", model.Total);
            command.Parameters.AddWithValue("@id", model.Id);
            command.ExecuteNonQuery();
        }
    }
}
