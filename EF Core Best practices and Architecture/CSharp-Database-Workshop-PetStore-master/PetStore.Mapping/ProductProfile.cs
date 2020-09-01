namespace PetStore.Mapping
{
    using System;
    using AutoMapper;

    using PetStore.Models;
    using PetStore.Models.Enumerations;
    using PetStore.ServiceModels.Products.InputModels;
    using PetStore.ServiceModels.Products.OutputModels;
    using PetStore.ViewModels.Products;
    using PetStore.ViewModels.Products.InputModels;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //Insert into DB
            this.CreateMap<AddProductInputServiceModel, Product>()
                .ForMember(x => x.ProductType, y => y.MapFrom(x => (ProductType)x.ProductType));

            this.CreateMap<EditProductInputServiceModel, Product>()
                .ForMember(x => x.ProductType, y => y.MapFrom(x => Enum.Parse(typeof(ProductType), x.ProductType)));

            this.CreateMap<CreateProductInputModel, AddProductInputServiceModel>();

            //List result
            this.CreateMap<Product, ListAllProductsByProductTypeServiceModel>();

            this.CreateMap<Product, ListAllProductsServiceModel>()
                .ForMember(x => x.ProductId, y => y.MapFrom(s => s.Id.ToString()))
                .ForMember(x => x.ProductType, y => y.MapFrom(x => x.ProductType.ToString()));

            this.CreateMap<Product, ListAllProductsByNameServiceModel>()
                .ForMember(x => x.ProductType, y => y.MapFrom(x => x.ProductType.ToString()));

            this.CreateMap<ListAllProductsServiceModel, ListAllProductsViewModel>();
            
        }
    }
}
