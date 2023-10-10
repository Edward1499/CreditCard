using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
        public virtual List<CreditCard> CreditCards { get; set; }
    }
}
