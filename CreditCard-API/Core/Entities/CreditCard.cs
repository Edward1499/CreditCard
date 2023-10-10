using Core.Common;
using Core.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CreditCard : BaseEntity
    {
        public int ClientId { get; set; }
        public Banks Bank { get; set; }
        public CreditCardTypes Type { get; set; }
        public string Number { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
