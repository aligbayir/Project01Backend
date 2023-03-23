using BusinessLayer.AutoMappers.InvoiceViewModels;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IInvoiceService
    {
        List<InvoiceViewModel> GetAll();
        Invoice GetById(int id);
        string Add(InvoiceViewModel invoice);
        string Update(InvoiceViewModel invoice);
        //string Delete(InvoiceViewModel invoice);
    }
}
