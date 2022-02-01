using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public ActionResult<IEnumerable<string>> Invoices()
        {
            return Ok(_invoiceService.GetAll());
        }
        [HttpGet("{id}")]
        [ActionName("Get")]
        public ActionResult<IEnumerable<string>> Invoices(int id)
        {
            return Ok(_invoiceService.Get(id));
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult<IEnumerable<string>> Invoices(Invoice model)
        {
            _invoiceService.Create(model);
            return Ok(model);
        }
        [HttpPatch]
        [ActionName("Update")]
        public ActionResult<IEnumerable<string>> Update(Invoice model)
        {
            _invoiceService.Update(model);
            return Ok(model);
        }
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public ActionResult<IEnumerable<string>> Delete(int id)
        {
            _invoiceService.Delete(id);
            return Ok(id);
        }
    }
}
