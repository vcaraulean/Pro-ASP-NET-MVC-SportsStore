using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Index_contains_all_products()
        {
            var controller = new AdminController(CreateRepositoryMock().Object);

            var result = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
        }

        [TestMethod]
        public void Can_edit_product()
        {
            var mock = new Mock<IProductRepository>();
            var controller = new AdminController(mock.Object);

            var product = new Product { Name = "Test" };

            var result = controller.Edit(product);
            mock.Verify(m => m.Save(product));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_save_invalid_changes()
        {
            var mock = new Mock<IProductRepository>();
            var controller = new AdminController(mock.Object);
            var product = new Product { Name = "Test" };

            controller.ModelState.AddModelError("error", "error");

            var result = controller.Edit(product);
            mock.Verify(m => m.Save(product), Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Delete_product_called_with_right_parameters()
        {
            var mock = CreateRepositoryMock();
            var controller = new AdminController(mock.Object);

            controller.Delete(2);

            mock.Verify(m => m.Delete(2));
        }

        private static Mock<IProductRepository> CreateRepositoryMock()
        {
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(r => r.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name = "P1", Category = "Apples"},
                new Product{ProductID = 2, Name = "P2", Category = "Oranges"},
                new Product{ProductID = 3, Name = "P3", Category = "Apples"},
            }.AsQueryable());
            return mockRepository;
        }
    }
}
