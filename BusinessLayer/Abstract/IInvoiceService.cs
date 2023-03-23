using BusinessLayer.AutoMappers.InvoiceViewModels;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IInvoiceService
    {
        List<InvoiceViewModel> GetAll(Expression<Func<Invoice,bool>> filter=null);
        Invoice GetById(int id);
        //List<InvoiceViewModel> GtInvoiceByCustomerId(int id);
        string Add(InvoiceViewModel invoice);
        string Update(InvoiceViewModel invoice);
    }
}
