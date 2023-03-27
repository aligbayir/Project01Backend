using BusinessLayer.AutoMappers.InvoiceViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Validators.InvoiceValidators
{
    public class InvoiceValidator:AbstractValidator<InvoiceViewModel>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber).NotEmpty().MinimumLength(2);
            RuleFor(x=>x.InvoiceAmount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}
