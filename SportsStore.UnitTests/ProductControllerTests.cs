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
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID = 1, Name = "P1"},
                new Product{ ProductID = 2, Name = "P2"},
                new Product{ ProductID = 3, Name = "P3"},
                new Product{ ProductID = 4, Name = "P4"},
                new Product{ ProductID = 5, Name = "P5"},
                new Product{ ProductID = 6, Name = "P6"},
            }.AsQueryable());

            var controller = new ProductController(mock.Object)
            {
                PageSize = 2
            };

            var viewModel = (ProductListViewModel)controller.List(2).Model;
            var pageInfo = viewModel.PagingInfo;

            Assert.AreEqual(6, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.ItemsPerPage);
            Assert.AreEqual(3, pageInfo.TotalPages);
            Assert.AreEqual(2, pageInfo.CurrentPage);
        }
    }
}
