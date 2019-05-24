using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplication.Managers;
using WebApplication.Models;

namespace WebApplication.Test.TDD.Controllers
{
    [TestFixture]
    public class LoginControllerShould
    {
        private static readonly string RETURN_PAGE = "RETURN_PAGE";

        
        [Test]
        public void Login_Should_returns_login_view()
        {
            
            var loginController = new LoginController();
            var result = loginController.Login(RETURN_PAGE) as ViewResult;

            Assert.AreEqual("Login", result.ViewName);
        }

        [Test]
        public void Login_Should_be_set_viewData_with_returnPage()
        {

            var loginController = new LoginController();
            var result = loginController.Login(RETURN_PAGE) as ViewResult;

            Assert.IsTrue(result.ViewData.Keys.Contains("ReturnPage"));
            Assert.AreEqual(result.ViewData["ReturnPage"], RETURN_PAGE);
        }
    }
}
