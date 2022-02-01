using Models;
using Repository.Interfaces;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.SqlServer
{
    public class InvoiceDetailRepository : Repository, IInvoiceDetailRepository
    {
        public InvoiceDetailRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }       
        public InvoiceDetail Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvoiceDetail> GetAllByInvoiceId(int invoiceId)
        {
            var result = new List<InvoiceDetail>();
            var command = CreateCommand("select * from InvoiceDetail WITH(NOLOCK) where InvoiceId= @InvoiceId");
            command.Parameters.AddWithValue("@InvoiceId", invoiceId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new InvoiceDetail
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Iva = Convert.ToInt32(reader["Iva"]),
                        SubTotal = Convert.ToInt32(reader["SubTotal"]),
                        Total = Convert.ToInt32(reader["Total"])
                    });
                }
            }
            return result;
        }

        public void Create(IEnumerable<InvoiceDetail> model, int invoiceId)
        {
            foreach (var detail in model)
            {
                var query = "Insert into InvoiceDetail(InvoiceId, ProductId, Quantity, Price,  Iva, SubTotal, Total) values (@InvoiceId, @ProductId, @Quantity, @Price, @Iva, @SubTotal, @Total)";
                var command = CreateCommand(query);
                command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                command.Parameters.AddWithValue("@ProductId", detail.ProductId);
                command.Parameters.AddWithValue("@Quantity", detail.Quantity);
                command.Parameters.AddWithValue("@Price", detail.Price);
                command.Parameters.AddWithValue("@Iva", detail.Iva);
                command.Parameters.AddWithValue("@SubTotal", detail.SubTotal);
                command.Parameters.AddWithValue("@Total", detail.Total);
                command.ExecuteNonQuery();
            }
        }
        public void RemoveByInvoiceId(int invoiceId)
        {
            var query = "delete from InvoiceDetail where InvoiceId = @InvoiceId";
            var command = CreateCommand(query);
            command.Parameters.AddWithValue("@InvoiceId", invoiceId);
            command.ExecuteNonQuery();
        }
    }
}
