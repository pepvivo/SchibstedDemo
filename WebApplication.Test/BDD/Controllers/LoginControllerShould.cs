using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplication.Managers;
using WebApplication.Models;

namespace WebApplication.Test.BDD.Controllers
{
    [TestFixture]
    public class LoginControllerShould
    {
        private static string USER_NAME { get; set; }
        private static string USER_PSW { get; set; }
        private static string RETURN_PAGE { get; set; }

        private static Mock<UserManager> _mockUserManager;
        private static Mock<SessionManager> _mockSessionManager;

        private static User USER { get; set; }
        private static List<User> USERS { get; set; }

        [SetUp]
        public void Init()
        {
            USER_NAME = "USER_NAME";
            USER_PSW = "USER_PSW";
            RETURN_PAGE = "RETURN_PAGE";

            _mockUserManager = new Mock<UserManager>();
            _mockSessionManager = new Mock<SessionManager>();

            USER = new User
            {
                UserName = USER_NAME,
                Password = USER_PSW
            };

            USERS = new List<User>
            {
                USER,
                USER
            };
        }


        [Test]
        public void Login_Should_redirect_last_request_when_login_is_ok()
        {
            var loginData = new LoginData()
            {
                UserName = USER_NAME,
                Password = USER_PSW,
                ReturnPage = RETURN_PAGE
            };

            var expectedResult = RETURN_PAGE;

            _mockUserManager.Setup(f => f.Get(It.IsAny<string>())).Returns(USER).Verifiable();
            _mockSessionManager.Setup(f => f.AddUserToSession(It.IsAny<User>())).Verifiable();

            var loginController = new LoginController(_mockUserManager.Object, _mockSessionManager.Object);
            var result = loginController.Login(loginData) as RedirectResult;

            Assert.AreEqual(result.Url, expectedResult);

            _mockUserManager.Verify();
            _mockSessionManager.Verify();
        }

        [Test]
        public void Login_Should_redirect_to_welcome_page_when_login_is_ok_but_returnPage_is_empty()
        {
            var loginData = new LoginData()
            {
                UserName = USER_NAME,
                Password = USER_PSW,
                ReturnPage = string.Empty
            };

            var expectedResult = "/Welcome.html";

            _mockUserManager.Setup(f => f.Get(It.IsAny<string>())).Returns(USER).Verifiable();
            _mockSessionManager.Setup(f => f.AddUserToSession(It.IsAny<User>())).Verifiable();

            var loginController = new LoginController(_mockUserManager.Object, _mockSessionManager.Object);
            var result = loginController.Login(loginData) as RedirectResult;

            Assert.AreEqual(result.Url, expectedResult);

            _mockUserManager.Verify();
            _mockSessionManager.Verify();
        }

        [Test]
        public void Login_Should_return_a_error_message_if_password_is_not_ok()
        {
            var loginData = new LoginData()
            {
                UserName = USER_NAME,
                Password = "not the same USER_PSW",
                ReturnPage = RETURN_PAGE
            };

            var expectedResult = "User or Password entered are not corrects!";

            _mockUserManager.Setup(f => f.Get(It.IsAny<string>())).Returns(USER).Verifiable();

            var loginController = new LoginController(_mockUserManager.Object, _mockSessionManager.Object);
            var result = loginController.Login(loginData) as ViewResult;

            Assert.AreEqual("Login", result.ViewName);

            Assert.IsTrue(result.ViewData.Keys.Contains("Message"));
            Assert.AreEqual(result.ViewData["Message"], expectedResult);

            _mockUserManager.Verify();
        }
    }
}
