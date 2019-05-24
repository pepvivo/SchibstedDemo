using NUnit.Framework;
using System.Web.Mvc;
using WebApplication.Controllers;

namespace WebApplication.Test.TDD.Controllers
{
    [TestFixture]
    public class Page2ControllerShould
    {

        [Test]
        public void Page2Controller_Should_return_a_correct_view()
        {
            var controller = new Page2Controller();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

    }
}
