using Models;
using Services;
using System;
using System.Collections.Generic;
using UnitOfWork.SqlServer;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var unitOfWork = new UnitOfWorkSqlServer();
            var invoiceService = new InvoiceService(unitOfWork);
            var result = invoiceService.Get(4);
            var result2 = invoiceService.Get(5);
            var all = invoiceService.GetAll();

            //var invoice = new Invoice
            //{
            //    ClientId = 3,
            //    Detail = new List<InvoiceDetail>
            //    {
            //        new InvoiceDetail
            //        {
            //            ProductId = 1,
            //            Quantity = 2,
            //            Price = 35000
            //        },
            //        new InvoiceDetail
            //        {
            //            ProductId = 3,
            //            Quantity = 2,
            //            Price = 38500.50m
            //        }
            //    }
            //};
            //invoiceService.Create(invoice);

            //invoice update
            //var invoice = new Invoice
            //{
            //    Id = 18,
            //    ClientId = 3,
            //    Detail = new List<InvoiceDetail>
            //    {
            //        new InvoiceDetail
            //        {
            //            ProductId = 1,
            //            Quantity = 1,
            //            Price = 35000
            //        },
            //        new InvoiceDetail
            //        {
            //            ProductId = 3,
            //            Quantity = 1,
            //            Price = 38500.50m
            //        },
            //        new InvoiceDetail
            //        {
            //            ProductId = 3,
            //            Quantity = 2,
            //            Price = 38500.50m
            //        }
            //    }
            //};
            //invoiceService.Update(invoice);

            //invoice delete
            //invoiceService.Delete(18);

            #region pre Unit of Work
            //var result = orderService.GetAll();
            //var result1 = orderService.Get(2);
            //invoice create
            //var invoice = new Invoice
            //{
            //    ClientId = 3,
            //    Detail = new List<InvoiceDetail>
            //    {
            //        new InvoiceDetail
            //        {
            //            ProductId = 1,
            //            Quantity = 2,
            //            Price = 35000
            //        },
            //        new InvoiceDetail
            //        {
            //            ProductId = 3,
            //            Quantity = 2,
            //            Price = 38500.50m
            //        }
            //    }
            //};
            //invoiceService.Create(invoice);

            //invoice update
            //var invoice = new Invoice
            //{
            //    Id = 15,
            //    ClientId = 3,
            //    Detail = new List<InvoiceDetail>
            //    {
            //        new InvoiceDetail
            //        {
            //            ProductId = 1,
            //            Quantity = 1,
            //            Price = 35000
            //        },
            //        new InvoiceDetail
            //        {
            //            ProductId = 3,
            //            Quantity = 1,
            //            Price = 38500.50m
            //        },
            //        new InvoiceDetail
            //        {
            //            ProductId = 3,
            //            Quantity = 2,
            //            Price = 38500.50m
            //        }
            //    }
            //};
            //invoiceService.Update(invoice);

            //invoice delete
            //invoiceService.Delete(15);
            #endregion
            Console.Read();            
        }
    }
}
