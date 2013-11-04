using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Save(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            
            return View(product);
        }

        public ActionResult Create(Product product)
        {
            return View("Edit", new Product());
        }

        public ActionResult Delete(int productId)
        {
            var deletedProduct = repository.Delete(productId);
            if (deletedProduct != null)
                TempData["message"] = string.Format("{0} has bee deleted", deletedProduct.Name);

            return RedirectToAction("Index");
        }
    }
}
