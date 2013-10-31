using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class PaginatorTests
    {
        [TestMethod]
        public void CanGeneratePageLinks()
        {
            HtmlHelper helper = null;

            var pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page " + i;
            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegate);

            var expected = @"<a href=""Page 1"">1</a>" +
                @"<a class=""selected"" href=""Page 2"">2</a>" +
                @"<a href=""Page 3"">3</a>";
            Assert.AreEqual(expected, result.ToString());
        }
    }
}
