using System.Collections.Generic;

namespace SalesDataBase.Models
{
   
    public class Product
    {
       
        public Product()
        {
            this.Sales = new List<Sale>();            
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        //Products migration
        //4. Add column Description up to 250 symbols 
        //Migration name "ProductsAddColumnDescription" with default value 
        //"no description"
        //Add default value on constructor or DBContex- HasDefaultvalue(value)
        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
