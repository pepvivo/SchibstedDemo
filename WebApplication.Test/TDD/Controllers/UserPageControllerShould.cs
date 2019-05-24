using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.Modules;

namespace WebApplication.Test.TDD.Controllers
{
    [TestFixture]
    public class UserPageControllerShould
    {

        private static UserPageController _usersPageController;

        private static Mock<HttpClient> _mockHttpClient;
        private static Mock<HttpRequestBase> _mockHttpRequestBase;
        private static Mock<SessionManager> _mockSessionManager;
        private static Mock<HttpCall> _mockHttpCall;



        private static string USER_NAME { get; set; }
        private static string USER_PSW { get; set; }
        private static Uri URL;
        private static User USER { get; set; }
        private static List<User> USERS { get; set; }
        private static UserViewModel USER_VIEW_MODEL { get; set; }


        [SetUp]
        public void Init()
        {

            _mockHttpClient = new Mock<HttpClient>();
            _mockHttpRequestBase = new Mock<HttpRequestBase>();
            _mockSessionManager = new Mock<SessionManager>();
            _mockHttpCall = new Mock<HttpCall>();

            _usersPageController = new UserPageController(_mockHttpRequestBase.Object,
                                                          _mockHttpCall.Object);

            URL = new Uri("http://THE_URL.URL");
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

            USER_VIEW_MODEL = new UserViewModel(USER);
        }

        [Test]
        public async Task List_Should_return_a_List_view_with_a_list_of_users()
        {
            _mockHttpRequestBase.Setup(h => h.Url).Returns(URL);

            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject( USERS)) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpCall.Setup(c => c.Exec(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockResponse).Verifiable();

            var result = await (_usersPageController.List() as Task<ActionResult>);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            Assert.AreEqual((result as ViewResult).ViewName, "List");
            Assert.IsInstanceOf<List<User>>((result as ViewResult).Model);

            var objResult = (List<User>)(result as ViewResult).Model;
            Assert.IsTrue(objResult.Count.Equals(USERS.Count));

            _mockHttpCall.Verify();
        }

        [Test]
        public async Task Index_Should_return_a_Index_view_with_an_user()
        {
            _mockHttpRequestBase.Setup(h => h.Url).Returns(URL);

            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(USER)) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpCall.Setup(c => c.Exec(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockResponse).Verifiable();

            var result = await (_usersPageController.Index(USER_NAME) as Task<ActionResult>);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            Assert.AreEqual((result as ViewResult).ViewName, "Index");
            Assert.IsInstanceOf<User>((result as ViewResult).Model);

            var objResult = (User)(result as ViewResult).Model;
            Assert.AreEqual(objResult.UserName, USER_NAME);

            _mockHttpCall.Verify();
        }

        [Test]
        public void Create_Should_return_a_Create_view_with_an_empty_UserViewModel()
        {
            var result = _usersPageController.Create() as ActionResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            Assert.AreEqual((result as ViewResult).ViewName, "Create");
            Assert.IsInstanceOf<UserViewModel>((result as ViewResult).Model);

            var objResult = (UserViewModel)(result as ViewResult).Model;
            Assert.IsEmpty(objResult.UserName);

            _mockHttpCall.Verify();
        }

        [Test]
        public async Task Created_Should_return_a_correct_message()
        {
            _mockHttpRequestBase.Setup(h => h.Url).Returns(URL);

            var expectedResult = "User created ok!";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedResult) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpCall.Setup(c => c.Exec<User>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>())).ReturnsAsync(mockResponse).Verifiable();

            var result = await (_usersPageController.Created(USER_VIEW_MODEL) as Task<ActionResult>);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);

            Assert.IsTrue((result as ViewResult).ViewData.Keys.Contains("Message"));
            Assert.AreEqual((result as ViewResult).ViewData["Message"], expectedResult);

            _mockHttpCall.Verify();
        }

        [Test]
        public async Task Update_Should_return_a_correct_message()
        {
            _mockHttpRequestBase.Setup(h => h.Url).Returns(URL);

            var expectedResult = "User updated ok!";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedResult) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpCall.Setup(c => c.Exec<User>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>())).ReturnsAsync(mockResponse).Verifiable();

            var result = await (_usersPageController.Update(USER_VIEW_MODEL) as Task<ActionResult>);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);

            Assert.IsTrue((result as ViewResult).ViewData.Keys.Contains("Message"));
            Assert.AreEqual((result as ViewResult).ViewData["Message"], expectedResult);

            _mockHttpCall.Verify();
        }

        [Test]
        public async Task Details_Should_return_a_Details_view_with_a_filled_UserViewModel()
        {
            var expectedResult = new UserViewModel(USER);

            _mockHttpRequestBase.Setup(h => h.Url).Returns(URL);

            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResult)) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpCall.Setup(c => c.Exec(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockResponse).Verifiable();

            var result = await (_usersPageController.Details(USER_NAME) as Task<ActionResult>);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            Assert.AreEqual((result as ViewResult).ViewName, "Details");
            Assert.IsInstanceOf<UserViewModel>((result as ViewResult).Model);

            var objResult = (UserViewModel)(result as ViewResult).Model;
            Assert.AreEqual(objResult.UserName, expectedResult.UserName);

            _mockHttpCall.Verify();
        }

        [Test]
        public async Task Edit_Should_return_a_Edit_view_with_a_filled_UserViewModel()
        {
            var expectedResult = new UserViewModel(USER);

            _mockHttpRequestBase.Setup(h => h.Url).Returns(URL);

            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResult)) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpCall.Setup(c => c.Exec(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockResponse).Verifiable();

            var result = await (_usersPageController.Edit(USER_NAME) as Task<ActionResult>);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            Assert.AreEqual((result as ViewResult).ViewName, "Edit");
            Assert.IsInstanceOf<UserViewModel>((result as ViewResult).Model);

            var objResult = (UserViewModel)(result as ViewResult).Model;
            Assert.AreEqual(objResult.UserName, expectedResult.UserName);

            _mockHttpCall.Verify();
        }

        [Test]
        public async Task Delete_Should_return_a_correct_message()
        {
            _mockHttpRequestBase.Setup(h => h.Url).Returns(URL);

            var expectedResult = "User deleted ok!";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedResult) };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpCall.Setup(c => c.Exec(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockResponse).Verifiable();

            var result = await (_usersPageController.Delete(USER_NAME) as Task<ActionResult>);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);

            Assert.IsTrue((result as ViewResult).ViewData.Keys.Contains("Message"));
            Assert.AreEqual((result as ViewResult).ViewData["Message"], expectedResult);

            _mockHttpCall.Verify();
        }

    }
}
