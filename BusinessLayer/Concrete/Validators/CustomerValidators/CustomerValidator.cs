using BusinessLayer.AutoMappers.CustomerViewModels;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Validators.CustomerValidators
{
    public class CustomerValidator:AbstractValidator<CustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.customerName).NotEmpty().MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.customerEmail).EmailAddress().NotEmpty();
            RuleFor(x => x.customerPhone).NotEmpty();
        }
    }
}
