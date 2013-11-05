using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repository) 
        {
            this.repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            Func<Product, bool> categoriesFilter = p => category == null || p.Category == category;

            var viewModel = new ProductListViewModel
            {
                Products = repository
                                .Products
                                .Where(categoriesFilter)
                                .OrderBy(p => p.ProductID)
                                .Skip((page - 1) * PageSize)
                                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count(categoriesFilter),
                },
                CurrentCategory = category,
            };

            return View(viewModel);
        }

        public FileContentResult GetImage(int productId)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
                return null;

            return File(product.ImageData, product.ImageMimeType);
        }
    }
}
