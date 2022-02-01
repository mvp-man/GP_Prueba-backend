using Repository.Interfaces;
using Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public IInvoiceRepository InvoiceRepository { get; }
        public IClientRepository ClientRepository  { get; }
        public IProductRepository ProductRepository { get; }
        public IInvoiceDetailRepository InvoiceDetailRepository { get; }
        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            InvoiceRepository = new InvoiceRepository(context, transaction);
            ClientRepository = new ClientRepository(context, transaction);
            ProductRepository = new ProductRepository(context, transaction);
            InvoiceDetailRepository = new InvoiceDetailRepository(context, transaction);
        }
    }
}
