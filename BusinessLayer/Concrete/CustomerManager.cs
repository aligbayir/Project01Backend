using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CustomerManager: ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public string Add(Customer customer)
        {
            _customerDal.Add(customer);
            return "Customer Başarıyla Eklendi";
            
        }

        public string Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return "Başarıyla Silme İşlemi Gerçekleştirildi";
        }

        public List<Customer> GetAll()
        {
            return _customerDal.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customerDal.Get(x => x.customerId == id);
        }

        public string Update(Customer customer)
        {
            var existingCustomer = _customerDal.Get(x=>x.customerId==customer.customerId);
            if (existingCustomer != null) 
            {
                existingCustomer.customerName = customer.customerName;
                existingCustomer.customerEmail = customer.customerEmail;
                existingCustomer.customerPhone = customer.customerPhone;
                existingCustomer.customerIsActive = customer.customerIsActive;
                existingCustomer.createDateTime = customer.createDateTime;
                existingCustomer.updatedDateTime = Convert.ToDateTime(customer.updatedDateTime);
                _customerDal.Update(existingCustomer);
                return "Customer Updated Successfully";
            }
            return "Customer Cannot found";
        }
    }
}
