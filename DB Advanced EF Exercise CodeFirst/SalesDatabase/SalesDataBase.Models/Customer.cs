using System;
using System.Collections.Generic;

namespace SalesDataBase.Models
{
    public class Customer
    {
        //Customer:
        //CustomerId
        //Name(up to 100 characters, unicode)
        //Email(up to 80 characters, not unicode)
        //CreditCardNumber(string)
        //Sales

        public Customer()
        {
            this.Sales = new List<Sale>();
        }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CardNumber { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
