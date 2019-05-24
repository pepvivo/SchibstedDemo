using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
//using System.Web.Mvc;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.Modules;
using WebApplication.Modules.Interfaces;

namespace WebApplication.Controllers

{
    /// <summary>
    /// Controller for user resources
    /// </summary>
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController, IUsersController
    {
        private UserManager _userManager;
        private Autheniication _autheniication;


        /// <summary>
        /// Default constructor
        /// </summary>
        public UsersController()
        {
            _userManager = new UserManager(); 
            _autheniication = new Autheniication();

        }

        /// <summary>
        /// Dependecy Inyection constructor
        /// For testing purposes
        /// </summary>
        /// <param name="userManager">UserManager class to use</param>
        /// <param name="autheniication">Authorizatin class to use</param>
        public UsersController(UserManager userManager, Autheniication autheniication) : base()
        {
            _userManager = userManager;
            _autheniication = autheniication;
        }

        // GET api/users
        /// <summary>
        /// Get a List of users
        /// </summary>
        /// <returns>IHttpActionResult especifiquing the result of operation and a list of users</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<User>))]
        public virtual IHttpActionResult Get()
        {
            if (!_autheniication.IsAuthentificated(User))
                return Unauthorized();

            List<User> users = new List<User>();
            try
            {
                users = _userManager.GetAll();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

            return (users.Count == 0) ?
                (IHttpActionResult)NotFound() :
                (IHttpActionResult)Ok(users);
        }

        // GET api/users/{id}
        /// <summary>
        /// Get a user
        /// </summary>
        /// <param name="id">Name identificator of user</param>
        /// <returns>IHttpActionResult especifiquing the result of operation and a user</returns>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(User))]
        public virtual IHttpActionResult Get(string id)
        {
            if (!_autheniication.IsAuthentificated(User))
                return Unauthorized();

            User user = new User();
            try
            {
                user = _userManager.Get(id);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

            return (user == null) ?
                (IHttpActionResult)NotFound() :
                (IHttpActionResult)Ok(user);

        }

        // POST api/users
        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="value">Json string with user serialized</param>
        /// <returns>IHttpActionResult especifiquing the result of operation</returns>
        [HttpPost]
        [Route("")]
        public virtual IHttpActionResult Post([FromBody]string value)
        {
            if (!_autheniication.IsAuthentificated(User))
                return Unauthorized();

            User user = JsonConvert.DeserializeObject<User>(value);

            try
            {
                return (_userManager.Create(user)) ?
                    (IHttpActionResult)Ok() :
                    (IHttpActionResult)BadRequest("This user already exists.");
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        // PUT api/users
        /// <summary>
        /// Udate an user
        /// </summary>
        /// <param name="value">Json string with user serialized</param>
        /// <returns>IHttpActionResult especifiquing the result of operation</returns>
        [Route("")]
        [HttpPut]
        public virtual IHttpActionResult Put([FromBody]string value)
        {
            if (!_autheniication.IsAuthentificated(User))
                return Unauthorized();

            User user = JsonConvert.DeserializeObject<User>(value);

            try
            {
                return (_userManager.Update(user)) ?
                    (IHttpActionResult)Ok() :
                    (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        // DELETE api/users/{id}
        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="value">Name identificator of user</param>
        /// <returns>IHttpActionResult especifiquing the result of operation</returns>
        [Route("{id}")]
        [HttpDelete]
        public virtual IHttpActionResult Delete(string value)
        {
            if (!_autheniication.IsAuthentificated(User))
                return Unauthorized();

            try
            {
                if (_userManager.IsLastAdminUser(value))
                    return BadRequest("Last admin user cannot be deleted.");

                return (_userManager.Delete(value)) ?
                    (IHttpActionResult)Ok() :
                    (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

        }

    }
}