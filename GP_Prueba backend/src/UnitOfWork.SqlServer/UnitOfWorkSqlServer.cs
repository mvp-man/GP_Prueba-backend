using System;
using System.Collections.Generic;
using System.Text;
using UnitOfWork.Interfaces;
using Common;
using Microsoft.Extensions.Configuration;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServer : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        public UnitOfWorkSqlServer(IConfiguration configuration = null)
        {
            _configuration = configuration;
        }
        public IUnitOfWorkAdapter Create()
        {
            var connectionString = _configuration == null ? Parameters.ConnectionString : _configuration.GetValue<string>("SqlConnectionString");
            return new UnitOfWorkSqlServerAdapter(Parameters.ConnectionString);
        }
    }
}
