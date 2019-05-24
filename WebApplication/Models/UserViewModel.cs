using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace WebApplication.Models

{   /// <summary>
    /// Class for Edit Users
    /// </summary>
    public class UserViewModel
    {
        private static int numMaxRoles = Enum.GetValues(typeof(Role)).Length;

        public UserViewModel()
        {
            UserName = String.Empty;
            UserPassword = String.Empty;
            AssignableRoles = new List<bool>();
            for (int i = 0; i < numMaxRoles; i++)
            {
                AssignableRoles.Add(false);
            }

        }

        public UserViewModel(User user)
        {
            if (user != null)
            {
                UserName = user.UserName;
                UserPassword = string.Empty;
                AssignableRoles = GetAssignableRolesFromUser(user);
            }
        }

        /// <summary>
        /// Name of User
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must less than {1}.")]
        public string UserName { get; set; }

        /// <summary>
        /// Password of User
        /// </summary>
        [Required]
        [StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string UserPassword { get; set; }


        [Required]
        public List<bool> AssignableRoles { get; set; }

        public List<Role> GetUserRolesFromAssignableRoles()
        {
            List<Role> roles = new List<Role>();
            if (AssignableRoles != null)
            {

                for (int i = 0; i < AssignableRoles.Count; i++)
                {
                    if (AssignableRoles[i])
                    {
                        roles.Add((Role)i);
                    }
                }
            }
            return roles;
        }

        private List<bool> GetAssignableRolesFromUser(User user)
        {
            List<bool> assignableRoles = new List<bool>();

            for (int i = 0; i < numMaxRoles; i++)
            {
                assignableRoles.Add(false);
                assignableRoles[i] = user.Roles.Contains((Role)i);
            }
            return assignableRoles;

            
        }

    }
}