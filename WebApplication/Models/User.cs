using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using WebApplication.Modules;

namespace WebApplication.Models
{
    /// <summary>
    /// Class for manage Users
    /// </summary>
    public class User
    {
        private string password;

        public User()
        {
            UserName = string.Empty;
            Password = string.Empty;
            Roles = new List<Role>();
        }

        /// <summary>
        /// Name of User
        /// </summary>
        [JsonProperty]
        public string UserName { get; set; }

        /// <summary>
        /// Password of User
        /// </summary>
        [JsonProperty]
        public string Password
        {
            private get
            {
                return password;
            }

            set
            {
                password = value ?? string.Empty;
            }
        }

        /// <summary>
        /// List of User's Roles assignement
        /// </summary>
        [JsonProperty]
        public List<Role> Roles { get; set; }

        public bool IsPasswordOk(string psw)
        {
            return Password.Equals(psw ?? string.Empty);
        }

        public string[] GetStringArrayRoles()
        {
            return Roles.Select(r => r.ToString()).ToArray();
        }

        public string GetStringRoles()
        {
            return string.Join(", ", Roles.Select(r => r.ToString()));
        }

        public string ToAutentification()
        {
            return $"{UserName}:{Password}";
        }

        public bool IsAdmin()
        {
            return Roles.Contains(Role.Admin);
        }
    }

}



