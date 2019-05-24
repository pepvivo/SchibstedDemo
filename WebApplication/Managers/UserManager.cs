using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Modules.Interfaces;
using WebApplication.Repositories;

namespace WebApplication.Managers
{

    /// <summary>
    /// Internal Singleton Class for managing users from BBDD 
    /// </summary>
    public class UserManager
    {

        private static IUserDB users;
        private static readonly object singletonLock = new object();

        private static IUserDB UDB
        {
            get
            {
                {
                    lock (singletonLock)
                    {
                        if (users == null)
                            users = new UserDB();

                        return users;
                    }
                }
            }
        }

        /// <summary>
        /// Obtains the user referenced by Id
        /// </summary>
        /// <param name="name">User identificator</param>
        /// <returns>User demanded by his identificator</returns>
        public virtual User Get(string name)
        {
            return UDB.Get(name);
        }

        /// <summary>
        /// Obtains All users
        /// </summary>
        /// <returns>List of users</returns>
        public virtual List<User> GetAll()
        {
            return UDB.GetAll();
        }

        /// <summary>
        /// Create new User entity 
        /// </summary>
        /// <param name="UserData"></param>
        /// <returns>true if created, false if not</returns>
        public virtual bool Create(User UserData)
        {
            return UDB.Create(UserData);
        }

        /// <summary>
        /// Update User information
        /// </summary>
        /// <param name="UserData">User information to update</param>
        /// <returns>true if updated, false if not</returns>
        public virtual bool Update(User UserData)
        {
            return UDB.Update(UserData);
        }

        /// <summary>
        /// Delete the user referenced by Id
        /// </summary>
        /// <param name="name">User identificator</param>
        /// <returns>true if deleted, false if not</returns>
        public virtual bool Delete(string name)
        {
            return UDB.Delete(name);
        }

        /// <summary>
        /// Determines if user name is the last admin user
        /// </summary>
        /// <param name="name">User identificator</param>
        /// <returns>true if he is the last admin user, false if not</returns>
        public virtual bool IsLastAdminUser(string name)
        {
            if (!UDB.Get(name).IsAdmin())
                return false;

            return UDB.GetAll().Count(u => u.IsAdmin()) == 1;
        }
    }
}

//    /// <summary>
//    /// Singleton Class for managing users from BBDD 
//    /// </summary>
//    public class UserManager
//    {
//        private static readonly object singletonLock = new object();
//        private static readonly UserManager instance = null;

//        private UserDB Users;

//        private UserManager()
//        {
//            Users = new UserDB();
//        }

//        /// <summary>
//        /// Singleton instance of UserManager Class
//        /// </summary>
//        public static UserManager Instance
//        {
//            get
//            {
//                lock (singletonLock)
//                {
//                    return instance ?? new UserManager();
//                }
//            }
//        }

//        /// <summary>
//        /// Obtains the user referenced by Id
//        /// </summary>
//        /// <param name="name">User identificator</param>
//        /// <returns>User demanded by his identificator</returns>
//        public User Get(string name)
//        {
//            return Users.Get(name);
//        }

//        /// <summary>
//        /// Obtains All users
//        /// </summary>
//        /// <returns>List of users</returns>
//        public List<User> GetAll()
//        {
//            return Users.GetAll();
//        }

//        /// <summary>
//        /// Create new User entity 
//        /// </summary>
//        /// <param name="UserData"></param>
//        /// <returns>true if created, false if not</returns>
//        public bool Create(User UserData)
//        {
//            return Users.Create(UserData);
//        }

//        /// <summary>
//        /// Update User information
//        /// </summary>
//        /// <param name="UserData">User information to update</param>
//        /// <returns>true if updated, false if not</returns>
//        public bool Update(User UserData)
//        {
//            return Users.Update(UserData);
//        }

//        /// <summary>
//        /// Delete the user referenced by Id
//        /// </summary>
//        /// <param name="name">User identificator</param>
//        /// <returns>true if deleted, false if not</returns>
//        public bool Delete(string name)
//        {
//            return Users.Delete(name);
//        }

//        /// <summary>
//        /// Determines if user name is the last admin user
//        /// </summary>
//        /// <param name="name">User identificator</param>
//        /// <returns>true if he is the last admin user, false if not</returns>
//        public bool IsLastAdminUser(string name)
//        {
//            if (!Users.Get(name).IsAdmin())
//                return false;

//            return Users.GetAll().Count(u => u.IsAdmin()) == 1;
//        }
//    }
//}