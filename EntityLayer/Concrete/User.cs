using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User:IEntity
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
