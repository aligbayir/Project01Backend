using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Customer:IEntity
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public bool customerIsActive { get; set; }
        public DateTime createDateTime { get; set; }
        public DateTime? updatedDateTime { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
}
