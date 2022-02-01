using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAll();
        Invoice Get(int id);
        void Create(Invoice model);
        void Update(Invoice model);
        void Delete(int id);
    }
    public class InvoiceService : IInvoiceService
    {
        private IUnitOfWork _unitOfWork;
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Invoice> GetAll()
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.InvoiceRepository.GetAll();
                foreach (var record in records)
                {
                    record.Client = context.Repositories.ClientRepository.Get(record.ClientId);
                    record.Detail = context.Repositories.InvoiceDetailRepository.GetAllByInvoiceId(record.Id);
                    foreach (var item in record.Detail)
                    {
                        item.Product = context.Repositories.ProductRepository.Get(item.ProductId);
                    }
                }
                return records;
            }            
        }        
        public Invoice Get(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                var result = context.Repositories.InvoiceRepository.Get(id);
                result.Client = context.Repositories.ClientRepository.Get(result.ClientId);
                result.Detail = context.Repositories.InvoiceDetailRepository.GetAllByInvoiceId(result.Id);
                foreach (var item in result.Detail)
                {
                    item.Product = context.Repositories.ProductRepository.Get(item.ProductId);
                }
                return result;
            }

        }        
        public void Create(Invoice model)
        {
            PrepareInvoice(model);
            using (var context = _unitOfWork.Create())
            {
                //Header
                context.Repositories.InvoiceRepository.Create(model);
                //Detail
                context.Repositories.InvoiceDetailRepository.Create(model.Detail,model.Id);
                context.SaveChanges();
            }
        }
        public void Update(Invoice model)
        {
            PrepareInvoice(model);
            using(var context = _unitOfWork.Create())
            {
                //Header
                context.Repositories.InvoiceRepository.Update(model);
                //Detail
                context.Repositories.InvoiceDetailRepository.RemoveByInvoiceId(model.Id);
                context.Repositories.InvoiceDetailRepository.Create(model.Detail, model.Id);
                //Confirm changes
                context.SaveChanges();
            }
        }        
        public void Delete(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                context.Repositories.InvoiceRepository.Remove(id);
                context.SaveChanges();
            }
        }        
        private void PrepareInvoice(Invoice model)
        {
            foreach (var detail in model.Detail)
            {
                detail.SubTotal = detail.Quantity * detail.Price;
                detail.Iva = detail.SubTotal * Parameters.IvaRate;
                detail.Total = detail.SubTotal + detail.Iva;
            }
            model.SubTotal = model.Detail.Sum(x => x.SubTotal);
            model.Iva = model.Detail.Sum(x => x.Iva);            
            model.Total = model.Detail.Sum(x => x.Total);

        }        
    }
}
