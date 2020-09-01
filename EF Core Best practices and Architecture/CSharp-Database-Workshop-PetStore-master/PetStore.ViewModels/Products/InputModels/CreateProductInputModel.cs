namespace PetStore.ViewModels.Products.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PetStore.Common;
    
    public class CreateProductInputModel
    {
        [Required]
        [MinLength(GlobalConstants.ProductNameMinLength)]
        [MaxLength(GlobalConstants.ProductNameMaxLength)]
        public string Name { get; set; }

        public int ProductType { get; set; }

        [Range(GlobalConstants.SellableMinPrice, Double.MaxValue)]
        public decimal Price { get; set; }
    }
}
