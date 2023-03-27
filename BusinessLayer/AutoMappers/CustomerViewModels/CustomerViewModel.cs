using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AutoMappers.CustomerViewModels
{
    public class CustomerViewModel
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public bool customerIsActive { get; set; }
        public DateTime createDateTime { get; set; }
        public DateTime? updatedDateTime { get; set; }
    }
}
