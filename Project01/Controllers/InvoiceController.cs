using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project01.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpGet]
        public IActionResult GetInvoiceLists()
        {
            var invoiceList = new
            {
                status = true,
                message = "",
                data = _invoiceService.GetAll()
            };

            return Ok(invoiceList);
        }
        [HttpPost]
        public IActionResult Add(InvoiceViewModel invoice)
        {
            var cst = _invoiceService.Add(invoice);
            var response = new
            {
                status = "True",
                message = cst
            };
            return Ok(response);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody] InvoiceViewModel updatedInvoice)
        {
            var upt = _invoiceService.Update(updatedInvoice);
            var res = new
            {
                message = upt,
                data = updatedInvoice
            };
            return Ok(res);
        }
        [HttpGet]
        [Route("{id}")]
        public Invoice GetByID(int id)
        {
            return _invoiceService.GetById(id);
        }

        [HttpGet]
        [Route("GetInvoicesByCustomerId")]
        public IQueryable GetInvoicesByCustomerId()
        {
            ProjectDbContext dbcontext = new ProjectDbContext();


            var result = from t1 in dbcontext.Invoices
                         join t2 in dbcontext.Customers on t1.CustomerId equals t2.customerId
                         select new
                         {
                             customerId = t1.CustomerId,
                             invoiceId = t1.InvoiceId,
                             invoiceNumber = t1.InvoiceNumber,
                             invoiceAmount = t1.InvoiceAmount,
                             customerName = t2.customerName,
                         };
            return result;
        }
    }
}
