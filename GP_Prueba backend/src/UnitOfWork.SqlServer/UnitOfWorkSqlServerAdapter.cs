﻿using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerAdapter : IUnitOfWorkAdapter
    {
        private SqlConnection _context { get; set; }
        private SqlTransaction _transaction { get; set; }
        public IUnitOfWorkRepository Repositories { get; set; }
        public UnitOfWorkSqlServerAdapter(string connectionString)
        {
            _context = new SqlConnection(connectionString);
            _context.Open();

            _transaction = _context.BeginTransaction();
            Repositories = new UnitOfWorkSqlServerRepository(_context, _transaction);
        }

        void IDisposable.Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if(_context != null)
            {
                _context.Close();
                _context.Dispose();
            }
            Repositories = null;
        }

        void IUnitOfWorkAdapter.SaveChanges()
        {
            _transaction.Commit();
        }
    }
}
