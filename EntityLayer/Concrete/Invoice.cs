using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Invoice:IEntity
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int InvoiceAmount { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
