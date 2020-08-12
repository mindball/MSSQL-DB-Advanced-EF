namespace FastFood.Core.MappingConfiguration
{
    using System;
    using AutoMapper;
    using FastFood.Core.ViewModels.Categories;

    using FastFood.Models;
    using ViewModels.Positions;
    using ViewModels.Employees;
    using ViewModels.Items;
    using FastFood.Core.ViewModels.Orders;
    using FastFood.Models.Enums;
    using System.Globalization;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.CategoryName));
            
            this.CreateMap<Category, CategoryAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Employee
            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(dto => dto.PositionId,
                opt => opt.MapFrom(s => s.Id))
                .ForMember(dto => dto.PositionName,
                opt => opt.MapFrom(s => s.Name));

            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position, y =>
                y.MapFrom(x => x.Position.Name));

            //Items
            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y =>
                y.MapFrom(x => x.Category.Name));

            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId,
                y => y.MapFrom(s => s.Id))
                .ForMember(x => x.CategoryName,
                y => y.MapFrom(s => s.Name));

            //Orders
            this.CreateMap<Item, CreateOrderItemViewModel>()
                .ForMember(x => x.ItemId,
                y => y.MapFrom(y => y.Id))
                .ForMember(x => x.ItemName,
                y => y.MapFrom(s => s.Name));

            this.CreateMap<Employee, CreateOrderEmployeeViewModel>()
                .ForMember(x => x.EmployeeId,
                y => y.MapFrom(y => y.Id))
                .ForMember(x => x.EmployeeName,
                y => y.MapFrom(s => s.Name));

            this.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(x => x.DateTime,
                y => y.MapFrom(x => DateTime.Now))
                .ForMember(x => x.Type,
                y => y.MapFrom(x => OrderType.ToGo));

            this.CreateMap<CreateOrderInputModel, OrderItem>()
                .ForMember(x => x.ItemId,
                y => y.MapFrom(x => x.ItemId))
                .ForMember(x => x.Quantity,
                y => y.MapFrom(x => x.Quantity));

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.Employee,
                y => y.MapFrom(x => x.Employee.Name))
                .ForMember(x => x.DateTime,
                y => y.MapFrom(x => x.DateTime
                                    .ToString("D", CultureInfo.InvariantCulture)))
                .ForMember(x => x.OrderId,
                y => y.MapFrom(x => x.Id));
        }
    }
}
