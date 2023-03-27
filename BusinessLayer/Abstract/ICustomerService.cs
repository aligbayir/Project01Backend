using BusinessLayer.AutoMappers.CustomerViewModels;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetAll();
        Customer GetById(int id);
        string Add(CustomerViewModel customer);
        string Update(CustomerViewModel customer);
        string Delete(Customer customer);
    }
}
