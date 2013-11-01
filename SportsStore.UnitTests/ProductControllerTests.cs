using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            var mock = CreateRepository();

            var controller = new ProductController(mock.Object)
            {
                PageSize = 2
            };

            var viewModel = (ProductListViewModel)controller.List(null, 2).Model;
            var pageInfo = viewModel.PagingInfo;

            Assert.AreEqual(6, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.ItemsPerPage);
            Assert.AreEqual(3, pageInfo.TotalPages);
            Assert.AreEqual(2, pageInfo.CurrentPage);
        }

        [TestMethod]
        public void TotalCountForCategories()
        {
            var controller = new ProductController(CreateRepository().Object)
            {
                PageSize = 2,
            };

            var viewModel = (ProductListViewModel)controller.List("Cat1", 2).Model;
            var pageInfo = viewModel.PagingInfo;

            Assert.AreEqual(3, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }

        private static Mock<IProductRepository> CreateRepository()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product{ ProductID = 2, Name = "P2", Category = "Cat1"},
                new Product{ ProductID = 3, Name = "P3", Category = "Cat4"},
                new Product{ ProductID = 4, Name = "P4", Category = "Cat3"},
                new Product{ ProductID = 5, Name = "P5", Category = "Cat3"},
                new Product{ ProductID = 6, Name = "P6", Category = "Cat1"},
            }.AsQueryable());
            return mock;
        }
    }
}
