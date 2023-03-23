using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AutoMappers.InvoiceViewModels
{
    public class InvoiceViewModelForCustomerName
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
