using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApplication.Controllers;
using WebApplication.Managers;
using WebApplication.Models;
using System.Web.Http;
using Assert = NUnit.Framework.Assert;
using System.Web.Http.Results;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Principal;
using WebApplication.Modules;

namespace WebApplication.Test.TDD.Controllers.api
{
    /// <summary>
    ///  This test class uses dependence injection  
    ///  this Test method is used for testing purposes
    /// </summary>
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
        public void Get_Should_return_correct_statusCode_and_an_user()
        {
            var expectedResult =new OkNegotiatedContentResult<User>(USER, _usersController);

            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true);
            _mockUserManager.Setup(f => f.Get(It.IsAny<string>())).Returns(USER);


            var result = _usersController.Get(USER_NAME);

            Assert.AreEqual(((OkNegotiatedContentResult<User>)result).Content, expectedResult.Content);
        }

        [Test]
        public void Get_Should_return_NotFound_statucCode_if_no_user_exists()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true);
            _mockUserManager.Setup(f => f.Get(It.IsAny<string>())).Returns((User)null);


            var result = _usersController.Get(USER_NAME);

            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void GetAll_Should_return_correct_statusCode_and_a_list_of_users()
        {
            var expectedResult =new OkNegotiatedContentResult<List<User>>(USERS, new UsersController());

            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true);
            _mockUserManager.Setup(f => f.GetAll()).Returns(USERS);


            var result = _usersController.Get();

            Assert.AreEqual(((OkNegotiatedContentResult<List<User>>)result).Content, expectedResult.Content);
        }

        [Test]
        public void GetAll_Should_return_NotFound_if_no_users_exist()
        {
            var Empty_User_list = new List<User>();

            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true);
            _mockUserManager.Setup(f => f.GetAll()).Returns(Empty_User_list);


            var result = _usersController.Get();

            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public void Post_Should_return_correct_statusCode()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true);
            _mockUserManager.Setup(f => f.Create(It.IsAny<User>())).Returns(true);


            var result = _usersController.Post(JsonConvert.SerializeObject(USER));

            Assert.IsTrue(result is OkResult);
        }


        [Test]
        public void Put_Should_return_correct_statusCode()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true);
            _mockUserManager.Setup(f => f.Update(It.IsAny<User>())).Returns(true);


            var result = _usersController.Put(JsonConvert.SerializeObject(USER));

            Assert.IsTrue(result is OkResult);
        }

        [Test]
        public void Delete_Should_return_correct_statusCode()
        {
            _mockAutheniication.Setup(f => f.IsAuthentificated(It.IsAny<IPrincipal>())).Returns(true);
            _mockUserManager.Setup(f => f.Delete(It.IsAny<string>())).Returns(true);
            _mockUserManager.Setup(f => f.IsLastAdminUser(It.IsAny<string>())).Returns(false);


            var result = _usersController.Delete(USER_NAME);

            Assert.IsTrue(result is OkResult);
        }
    }
}
