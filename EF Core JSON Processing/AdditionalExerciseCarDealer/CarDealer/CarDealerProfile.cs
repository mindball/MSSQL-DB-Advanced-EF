namespace CarDealer
{
    using System.Linq;

    using AutoMapper;

    using CarDealer.Models;

    using CarDealer.DTO;
    using CarDealer.DTO.CarsWithTheirListOfParts;
    using CarDealer.DTO.TotalSalesByCustomer;
    using System.Security.Cryptography.X509Certificates;
    using CarDealer.DTO.SalesWithAppliedDiscount;
    using System.IO.Compression;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Customers
            this.CreateMap<Customer, OrderedCustomer>();

            this.CreateMap<Customer, CustomerSales>()
                .ForMember(c => c.BoughCarsCount,
                f => f.MapFrom(y => y.Sales.Count()))
                .ForMember(x => x.SpentMoney, 
                y => y.MapFrom(s => s.Sales
                            .SelectMany(z => z.Car.PartCars)
                            .Sum(p => p.Part.Price).ToString("f2")));

             //Cars
             this.CreateMap<Car, CarFromMakeToyota>();

            this.CreateMap<Car, CarInfoList>();
            this.CreateMap<Part, ListOfParts>();

            this.CreateMap<Car, CarDTO>()
                .ForMember(x => x.InfoCar,
                y => y.MapFrom(s => s))
                .ForMember(x => x.Parts,
                y => y.MapFrom(b => b.PartCars.Select(a => a.Part)));

            //Supplier
            this.CreateMap<Supplier, LocalSuppliers>()
                .ForMember(x => x.PartsCount,
                y => y.MapFrom(s => s.Parts.Count));

            //Sale
            this.CreateMap<Car, CarInfo>();

            this.CreateMap<Sale, SaleDTO>()
                 .ForMember(x => x.CarInfo,
                 y => y.MapFrom(s => s.Car))
                 .ForMember(x => x.Name,
                 y => y.MapFrom(s => s.Customer.Name))
                 .ForMember(x => x.Discount,
                 y => y.MapFrom(s => s.Discount))
                 .ForMember(x => x.Price,
                 y => y.MapFrom(s => s.Car.PartCars.Sum(y => y.Part.Price)))
                 .ForMember(x => x.PriceWithDiscount,
                 y => y.MapFrom(s =>
                 s.Car.PartCars.Sum(z => z.Part.Price) - 
                                    (s.Car.PartCars.Sum(z => z.Part.Price) 
                                    * (s.Discount / 100))));


        }
    }
}
