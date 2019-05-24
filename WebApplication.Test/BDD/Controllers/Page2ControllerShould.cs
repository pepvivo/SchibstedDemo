using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Controllers;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.Modules;

namespace WebApplication.Test.BDD.Controllers
{
    [TestFixture]
    public class Page2ControllerShould
    {
        private static string USER_NAME { get; set; }
        private static string USER_PSW { get; set; }
        private static string RETURN_PAGE { get; set; }

        private static Mock<SessionManager> _mockSessionManager;

        private static User USER { get; set; }

        [SetUp]
        public void Init()
        {
            USER_NAME = "USER_NAME";
            USER_PSW = "USER_PSW";
            RETURN_PAGE = "RETURN_PAGE";

            _mockSessionManager = new Mock<SessionManager>();

            USER = new User
            {
                UserName = USER_NAME,
                Password = USER_PSW,
            };

        }

        [Test]
        public void Page2Controller_Should_return_NotAuthorized_if_requested_user_hasnt_page2_role()
        {

            USER.Roles = new List<Role>() { Role.Page1 };

            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Items).Returns(new Dictionary<object, object>());
            context.Setup(c => c.User.Identity.IsAuthenticated).Returns(true);
            var controller = new Mock<Page2Controller>();

            var actionDescriptor = new Mock<ActionDescriptor>();
            actionDescriptor.Setup(a => a.ActionName).Returns("Index");

            var controllerDescriptor = new Mock<ControllerDescriptor>();
            actionDescriptor.Setup(a => a.ControllerDescriptor).Returns(controllerDescriptor.Object);
            var mockResponse = new Mock<HttpResponseBase>();


            var controllerContext = new ControllerContext(context.Object, new RouteData(), controller.Object);
            var filterContext = new AuthorizationContext(controllerContext, actionDescriptor.Object);

            _mockSessionManager.Setup(f => f.CurrentUser).Returns(USER);

            var att = new AuthorizedRolesAttribute(_mockSessionManager.Object, mockResponse.Object);

            att.OnAuthorization(filterContext);

            Assert.That(filterContext.Result, Is.InstanceOf<HttpUnauthorizedResult>());
        }

    }

}
