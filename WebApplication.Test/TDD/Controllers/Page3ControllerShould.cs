using NUnit.Framework;
using System.Web.Mvc;
using WebApplication.Controllers;

namespace WebApplication.Test.TDD.Controllers
{
    [TestFixture]
    public class Page3ControllerShould
    {

        [Test]
        public void Page3Controller_Should_return_a_correct_view()
        {
            var controller = new Page3Controller();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

    }
}
