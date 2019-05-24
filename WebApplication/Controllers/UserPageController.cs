using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Managers;
using WebApplication.Models;
using System.Net.Http.Formatting;
using WebApplication.Modules;

namespace WebApplication.Controllers
{
    [AuthorizedRoles(Roles = RolesConfig.ROLE_ADMIN)]
    public class UserPageController : SessionBaseController
    {

        private const string USERS_ROUTE = "/api/users";

        private HttpRequestBase _httpRequestBase;
        private HttpCall _httpCall;

        public UserPageController()
        {
            _httpRequestBase = null;
            _httpCall = new HttpCall();
    }

        public UserPageController(HttpRequestBase httpRequestBase, HttpCall httpCall)
        {
            _httpRequestBase = httpRequestBase;
            _httpCall = httpCall;
        }

        private HttpRequestBase GetRequest()
        {
            return (_httpRequestBase == null) ? Request : _httpRequestBase;
        }

        public virtual string BuildApiUrl(string route)
        {
            return $"{GetRequest().Url.Scheme}://{GetRequest().Url.Authority}" + route;
        }

        // GET: All Users
        public async Task<ActionResult> List()
        {
            List<User> users = new List<User>();
            string apiUrl = BuildApiUrl(USERS_ROUTE);


            HttpResponseMessage response = await _httpCall.Exec(apiUrl, HttpCall.GET);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(data);
            }
            return View("List", users);
        }


        public async Task<ActionResult> Index(string userName)
        {
            User user = new Models.User();
            string apiUrl = BuildApiUrl($"{USERS_ROUTE}/{userName}");

            HttpResponseMessage response = await _httpCall.Exec(apiUrl, HttpCall.GET);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(data);
            }
            return View("Index", user);
        }

        public ActionResult Create()
        {
            return View("Create", new UserViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Created(UserViewModel userViewModel)
        {
            string apiUrl = BuildApiUrl(USERS_ROUTE);

            var user = new User()
            {
                UserName = userViewModel.UserName,
                Password = userViewModel.UserPassword,
                Roles = userViewModel.GetUserRolesFromAssignableRoles()
            };

            HttpResponseMessage response = await _httpCall.Exec<User>(apiUrl, HttpCall.POST, user);

            var responseMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ViewBag.Message = response.IsSuccessStatusCode ? 
                Resources.AppMessages.user_created_ok  : 
                $"{Resources.AppMessages.user_not_created} {response.ReasonPhrase} - {responseMessage}";

            return View("Message");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UserViewModel userViewModel)
        {
            string apiUrl = BuildApiUrl(USERS_ROUTE);

            var user = new User()
            {
                UserName = userViewModel.UserName,
                Password = userViewModel.UserPassword,
                Roles = userViewModel.GetUserRolesFromAssignableRoles()
            };

            HttpResponseMessage response = await _httpCall.Exec<User>(apiUrl, HttpCall.PUT, user);

            var responseMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ViewBag.Message = response.IsSuccessStatusCode ? 
                Resources.AppMessages.user_updated_ok : 
                $"{Resources.AppMessages.user_not_updated} {response.ReasonPhrase} - {responseMessage}";

            return View( "Message");
        }

        [HttpGet]
        public async Task<ActionResult> Details(string name)
        {
            if (name != null)
            {
                User user = new Models.User();
                string apiUrl = BuildApiUrl($"{USERS_ROUTE}/{name}");

                HttpResponseMessage response = await _httpCall.Exec(apiUrl, HttpCall.GET);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(data);
                    return View("Details", new UserViewModel(user));
                }
            }

            return View("Details", new UserViewModel());

        }

        public async Task<ActionResult> Edit(string name)
        {
            User user = new Models.User();
            string apiUrl = BuildApiUrl($"{USERS_ROUTE}/{name}");

            HttpResponseMessage response = await _httpCall.Exec(apiUrl, HttpCall.GET);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(data);
            }
            return View("Edit", new UserViewModel(user));
        }

        public async Task<ActionResult> Delete(string name)
        {

            string apiUrl = BuildApiUrl($"{USERS_ROUTE}/{name}");

            HttpResponseMessage response = await _httpCall.Exec(apiUrl, HttpCall.DELETE);

            var responseMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ViewBag.Message = response.IsSuccessStatusCode ? 
                Resources.AppMessages.user_deleted_ok : 
                $"{Resources.AppMessages.user_not_deleted} {response.ReasonPhrase} - {responseMessage}";

            return View("Message");
        }
    }
}

