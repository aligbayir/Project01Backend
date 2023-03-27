using AutoMapper;
using BusinessLayer.AutoMappers.CustomerViewModels;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using BusinessLayer.AutoMappers.UserViewModels;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AutoMappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User,UserLoginViewModel>();
            CreateMap<Invoice,InvoiceViewModel>();
            CreateMap<InvoiceViewModel, Invoice>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();
            //CreateMap<Invoice,InvoiceViewModelForCustomerName>();
            //CreateMap<InvoiceViewModelForCustomerName, Invoice>();

        }
    }
}
