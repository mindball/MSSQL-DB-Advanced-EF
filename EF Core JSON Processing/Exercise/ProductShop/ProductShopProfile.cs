
namespace ProductShop
{
    using AutoMapper;

    using Models;
    using DTO.Product;
    using DTO.User;
    using DTO.Category;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
       
        public ProductShopProfile()
        {
            //Products
            this.CreateMap<Product, ProductWithRangeDTO>()
                .ForMember(s => s.SellerName,
                y => y.MapFrom(x => x.Seller.FirstName + x.Seller.LastName));


            //User
            this.CreateMap<Product, UserSoldItem>();

            this.CreateMap<User, UserNameWithSoldItemsDTO>()
                .ForMember(s => s.SoldItems,
                y => y.MapFrom(p => p.ProductsSold
                                        .Where(x => x.Buyer != null)));

            //Category
            this.CreateMap<Category, GetAllCategoryWithProductCount>()
                .ForMember(s => s.ProductCount,
                y => y.MapFrom(p => p.CategoryProducts.Count()))
                .ForMember(s => s.AveragePrice,
                y => y.MapFrom(p => p.CategoryProducts
                                        .Average(x => x.Product.Price)))
                .ForMember(s => s.TotalPrice,
                y => y.MapFrom(p => p.CategoryProducts
                                        .Sum(x => x.Product.Price)));

            //Може да ползваме закръгляне до две числа след дес. запетая
            //описва се в profile-CreateMap .ToString("f2")\
            //Aко пропъртита са string
            //this.CreateMap<Category, GetAllCategoryWithProductCount>()
            //   .ForMember(s => s.ProductCount,
            //   y => y.MapFrom(p => p.CategoryProducts.Count()))
            //   .ForMember(s => s.StringAveragePrice,
            //   y => y.MapFrom(p => p.CategoryProducts
            //                           .Average(x => x.Product.Price).ToString("f2")))
            //   .ForMember(s => s.StringTotalPrice,
            //   y => y.MapFrom(p => p.CategoryProducts
            //                           .Sum(x => x.Product.Price).ToString("f2")));
        }
    }
}
