﻿using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
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
            var customer = _invoiceService.GetById(invoice.CustomerId);
            if (customer != null)
            {
            var cst = _invoiceService.Add(invoice);
            var response = new
            {
                status = "True",
                message = cst
            };
            return Ok(response);
               
            }
            return NotFound();



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
        public IActionResult GetByID(int id)
        {
            var invoice = _invoiceService.GetById(id);
            if (invoice != null)
            {
                return Ok(invoice);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetInvoicesWithCustomer")]
        public IActionResult GetInvoicesWithCustomer()
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

            //var result = dbcontext.Invoices.Include(x => x.Customer).Where(x => x.CustomerId == id).Select(x => x.Customer.customerName);
            return Ok(result.ToList());
        }
        [HttpGet]
        [Route("GetInvoiceWithCustomer/{id}")]
        public IActionResult GetInvoiceWithCustomer(int id)
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
            var selected = result.Where(x => x.invoiceId == id).Select(x => new { x.invoiceId, x.invoiceNumber, x.invoiceAmount, x.customerName, x.customerId }).ToList();

            //var result = dbcontext.Invoices.Include(x => x.Customer).Where(x => x.CustomerId == id).Select(x => x.Customer.customerName);
            return Ok(selected);
        }
        [HttpGet]
        [Route("GetInvoiceByCustomerId/{id}")]
        public IActionResult GetInvoicesWithCustomer(int id)
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
            var selected = result.Where(x => x.customerId == id);

            //var result = dbcontext.Invoices.Include(x => x.Customer).Where(x => x.CustomerId == id).Select(x => x.Customer.customerName);
            return Ok(selected.ToList());
        }
    }
}
