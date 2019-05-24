using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication.Controllers;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.Modules;
using WebApplication.Modules.Interfaces;
using WebApplication.Repositories;

namespace WebApplication.Test.BDD.Controllers.api
{
    [TestFixture]
    internal class UsersControllerShould
    {
        private static Mock<UserManager> _mockUserManager;
        private static Mock<Autheniication> _mockAutheniication;

        private static UsersController _usersController;


        private static string USER_NAME { get; set; }
        private static string USER_PSW { get; set; }
        private static User USER { get; set; }
        private static List<User> USERS { get; set; }

/*
        private const string GET = "GET";
        private const string POST = "POST";
        private const string PUT = "PUT";
        private const string DELETE = "DELETE";
*/

        [SetUp]
        public void Init()
        {
            _mockUserManager = new Mock<UserManager>();
            _mockAutheniication = new Mock<Autheniication>();

            _usersController = new UsersController(_mockUserManager.Object, _mockAutheniication.Object);

            USER_NAME = "USER_NAME";
            USER_PSW = "USER_PSW";

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
        public void Get_Should_return_Unauthorized_when_user_is_not_authentificated()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(false).Verifiable(); 

            var result = _usersController.Get(USER_NAME);

            Assert.IsInstanceOf< UnauthorizedResult> (result);

            _mockAutheniication.Verify();
        }

        [Test]
        public void GetAll_Should_return_Unauthorized_when_user_is_not_authentificated()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(false).Verifiable();

            var result = _usersController.Get();

            Assert.IsInstanceOf<UnauthorizedResult>(result);

            _mockAutheniication.Verify();
        }

        [Test]
        public void Post_Should_return_Unauthorized_when_user_is_not_authentificated()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(false).Verifiable();

            var result = _usersController.Post(JsonConvert.SerializeObject(USER));

            Assert.IsInstanceOf<UnauthorizedResult>(result);

            _mockAutheniication.Verify();
        }

        [Test]
        public void Put_Should_return_Unauthorized_when_user_is_not_authentificated()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(false).Verifiable();

            var result = _usersController.Put(JsonConvert.SerializeObject(USER));

            Assert.IsInstanceOf<UnauthorizedResult>(result);

            _mockAutheniication.Verify();
        }

        [Test]
        public void Delete_Should_return_Unauthorized_when_user_is_not_authentificated()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(false).Verifiable();

            var result = _usersController.Delete(USER_NAME);

            Assert.IsInstanceOf<UnauthorizedResult>(result);

            _mockAutheniication.Verify();
        }

        [Test]
        public void Delete_Should_return_BadRequest_when_we_want_delete_last_admin_user()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true).Verifiable();
            _mockUserManager.Setup(f => f.IsLastAdminUser(It.IsAny<string>())).Returns(true).Verifiable();

            var result = _usersController.Delete(USER_NAME);

            Assert.IsTrue(result is BadRequestErrorMessageResult);

            _mockAutheniication.Verify();
            _mockUserManager.Verify();
        }


    }
}
