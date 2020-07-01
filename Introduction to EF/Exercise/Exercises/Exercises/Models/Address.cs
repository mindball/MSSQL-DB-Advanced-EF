using System.Collections.Generic;

namespace Exercises.Models
{
    public partial class Address
    {
        public Address()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int AddressId { get; set; }

        public string AddressText { get; set; }

        public int? TownId { get; set; }


        public  Town Town { get; set; }

        public  ICollection<Employee> Employees { get; set; }
    }
}
