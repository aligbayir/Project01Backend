using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class InvoiceManager : IInvoiceService
    {
        private readonly IInvoiceDal _invoiceDal;
        private readonly ICustomerDal _customerDal;
        private readonly IMapper _mapper;

        public InvoiceManager(IInvoiceDal invoiceDal, IMapper mapper, ICustomerDal customerDal)
        {
            _invoiceDal = invoiceDal;
            _mapper = mapper;
            _customerDal = customerDal;
        }

        public string Add(InvoiceViewModel invoice)
        {
            var invc = _mapper.Map<Invoice>(invoice);
            _invoiceDal.Add(invc);
            return "Fatura Başarıyla eklendi";
        }

        public List<InvoiceViewModel> GetAll(Expression<Func<Invoice, bool>> filter = null)
        {
            return _invoiceDal.GetAll(filter).Select(x => _mapper.Map<InvoiceViewModel>(x)).ToList();
        }

        public Invoice GetById(int id)
        {
            return _invoiceDal.Get(x => x.InvoiceId == id);
        }
        public string Update(InvoiceViewModel invoice)
        {
            var existingInvoice = _invoiceDal.Get(x => x.InvoiceId == invoice.InvoiceId);
            if (existingInvoice != null)
            {
                existingInvoice.CustomerId = invoice.CustomerId;
                existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
                existingInvoice.InvoiceAmount = Convert.ToInt32(invoice.InvoiceAmount);
                _invoiceDal.Update(existingInvoice);
                return "Invoice Updated Successfully";
            }
            return "Invoice Cannot found";
        }
    }
}
