using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDataBase.Models
{
    public class Store
    {
        //        StoreId
        //Name(up to 80 characters, unicode)
        //Sales

        public Store()
        {
            this.Sales = new List<Sale>();
        }

        public int StoreId { get; set; }

        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
