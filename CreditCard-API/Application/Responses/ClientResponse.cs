﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
        public List<CreditCard> CreditCards { get; set; }
    }
}