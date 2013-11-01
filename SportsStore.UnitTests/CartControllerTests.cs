using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Controllers;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void Can_add_to_cart()
        {
            var mockRepository = CreateRepositoryMock();

            var controller = new CartController(mockRepository.Object);
            var cart = new Cart();

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(1, cart.Lines.Count());
            Assert.AreEqual(cart.Lines.ElementAt(0).Product.ProductID, 1);
        }

        [TestMethod]
        public void Adding_product_goes_to_cart_screen()
        {
            var cart = new Cart();
            var controller = new CartController(CreateRepositoryMock().Object);

            var result = controller.AddToCart(cart, 2, "my url");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "my url");
        }

        [TestMethod]
        public void Can_view_cart_contents()
        {
            var cart = new Cart();
            var controller = new CartController(null);

            var result = (CartIndexViewModel)controller.Index(cart, "myurl").ViewData.Model;

            Assert.AreSame(cart, result.Cart);
            Assert.AreEqual("myurl", result.ReturnUrl);
        }

        private static Mock<IProductRepository> CreateRepositoryMock()
        {
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(r => r.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name = "P1", Category = "Apples"},
            }.AsQueryable());
            return mockRepository;
        }
    }
}
