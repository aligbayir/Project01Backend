using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class InvoiceManager : IInvoiceService
    {
        private readonly IInvoiceDal _invoiceDal;
        private readonly IMapper _mapper;

        public InvoiceManager(IInvoiceDal invoiceDal, IMapper mapper)
        {
            _invoiceDal = invoiceDal;
            _mapper = mapper;
        }

        public string Add(InvoiceViewModel invoice)
        {
            var invc = _mapper.Map<Invoice>(invoice);
            _invoiceDal.Add(invc);
            return "Fatura Başarıyla eklendi";
        }

        //public string Delete(InvoiceViewModel invoice)
        //{
        //    var siparisbulunan = _invoiceDal.Get(x => x.InvoiceId == id);
        //    _siparisDal.Delete(siparisbulunan);
        //    return "Başarıyla Silme İşlemi Gerçekleştirildi.";
        //}

        public List<InvoiceViewModel> GetAll()
        {
            return _invoiceDal.GetAll().Select(x => _mapper.Map<InvoiceViewModel>(x)).ToList();
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
                existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
                existingInvoice.InvoiceAmount = Convert.ToInt32(invoice.InvoiceAmount);
                _invoiceDal.Update(existingInvoice);
                return "Invoice Updated Successfully";
            }
            return "Invoice Cannot found";
        }
    }
}
