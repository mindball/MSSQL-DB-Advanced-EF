namespace CarDealer
{
    using System.Linq;

    using AutoMapper;
    using CarDealer.DTO;
    using CarDealer.Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Customers
            this.CreateMap<Customer, OrderedCustomer>();

            //Cars
            this.CreateMap<Car, CarFromMakeToyota>();
        }
    }
}
