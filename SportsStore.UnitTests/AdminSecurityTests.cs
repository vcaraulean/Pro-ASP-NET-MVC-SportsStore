using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_login_with_valid_credentials()
        {
            var mock = new Mock<IAuthProvider>();
            mock.Setup(p => p.Authenticate("adm", "sec")).Returns(true);

            var loginViewModel = new LoginViewModel
            {
                Username = "adm",
                Password = "sec"
            };

            var controller = new AccountController(mock.Object);
            var result = controller.Login(loginViewModel, "my-url");

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("my-url", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_login_with_bad_credentials()
        {
            var mock = new Mock<IAuthProvider>();
            mock.Setup(p => p.Authenticate("bad user", "bad pass")).Returns(false);

            var loginViewModel = new LoginViewModel
            {
                Username = "bad user",
                Password = "bad pass"
            };

            var controller = new AccountController(mock.Object);
            var result = controller.Login(loginViewModel, "my-url");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
