namespace ProductShop
{
    using AutoMapper;

    using ProductShop.DTO.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //Import DB
            this.CreateMap<ImportUserDto, User>();

            this.CreateMap<ImportProductDto, Product>();

            this.CreateMap<ImportCategoryDto, Category>();

            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();

            /* Export from DB
             */
            //Products
            this.CreateMap<Product, ProductInRangeDto>()
                .ForMember(x => x.Buyer,
                y => y.MapFrom(s => s.Buyer.FirstName + " " + s.Buyer.LastName));

        }
    }
}
