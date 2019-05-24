using NUnit.Framework;
using System.Web.Mvc;
using WebApplication.Controllers;

namespace WebApplication.Test.TDD.Controllers
{
    [TestFixture]
    public class HomeControllerShould
    {
        [Test]
        public void Home_Should_returns_home_index_view()
        {
            var homeController = new HomeController();
            var result = homeController.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

    }
}
