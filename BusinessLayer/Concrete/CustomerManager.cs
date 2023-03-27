using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.CustomerViewModels;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using BusinessLayer.Concrete.Validators.CustomerValidators;
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
    public class CustomerManager: ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDal customerDal, IMapper mapper)
        {
            _customerDal = customerDal;
            _mapper = mapper;
        }

        public string Add(CustomerViewModel customer)
        {
            var invc = _mapper.Map<Customer>(customer);
            _customerDal.Add(invc);
            return "Customer Başarıyla Eklendi";
            
        }

        public string Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return "Başarıyla Silme İşlemi Gerçekleştirildi";
        }

        public List<CustomerViewModel> GetAll()
        {
            return _customerDal.GetAll().Select(x=> _mapper.Map<CustomerViewModel>(x)).ToList();
        }

        public Customer GetById(int id)
        {
            return _customerDal.Get(x => x.customerId == id);
        }

        public string Update(CustomerViewModel customer)
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
