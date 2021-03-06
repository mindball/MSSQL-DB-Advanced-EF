﻿

namespace PetStore.Web.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    
    using AutoMapper;
    
    using PetStore.Services.Interfaces;
    using PetStore.ServiceModels.Products.InputModels;

    using PetStore.ViewModels.Products;
    using PetStore.ViewModels.Products.InputModels;
    using PetStore.ViewModels.Products.OutputModels;

    public class ProductController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private IProductService productService;
        private IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult All()
        {
            var allProducts = this.productService.GetAll().ToList();

            var viewAllProducts = this.mapper.Map<List<ListAllProductsViewModel>>(allProducts);

            return View(viewAllProducts);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var serviceModel = this.mapper.Map<AddProductInputServiceModel>(model);

            this.productService.AddProduct(serviceModel);

            return this.RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var serviceModel = this.productService.GetById(id);

            var productDetails = this.mapper.Map<ProductDetailsViewModel>(serviceModel);

            return this.View(productDetails);
        }

        [HttpGet]
        public IActionResult Search(string searchWord)
        {
            if(searchWord == null 
                || string.IsNullOrWhiteSpace(searchWord))
            {
                return this.RedirectToAction("All");
            }
            var searchProducts = this.productService.SearchByName(searchWord, false);

            var searchProductModel = this.mapper.Map<List<ListAllProductsViewModel>>(searchProducts);

            return this.View("All", searchProductModel);
        }

    }
}
