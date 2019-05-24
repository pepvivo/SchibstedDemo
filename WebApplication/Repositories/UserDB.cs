using System;
using System.Collections.Generic;
using WebApplication.Modules;
using WebApplication.Models;
using WebApplication.Modules.Interfaces;

namespace WebApplication.Repositories
{
    /// <summary>
    /// Class Connection to Database
    /// (In this case JSON text file 'Database')
    /// 
    /// </summary>
    public class UserDB : IUserDB
    {
        private const string USER_FILE_EXTENSION = "user";
        private const string DB_PATH = "DDBB";
        private const string USER_DB_PATH = DB_PATH + @"\Users";

        #region "Properties and Constructor"

        private string UserDBPath { get; }
        private IIOFile IoFile { get; }

        public UserDB()
        {
            IoFile = new IOFile();
            UserDBPath = AppContext.BaseDirectory + USER_DB_PATH;
            IoFile.CreateDirectory(UserDBPath);
        }

        /// <summary>
        /// Special constructor for Test
        /// </summary>
        /// <param name="ioFile"></param>
        public UserDB(IIOFile ioFile)
        {
            IoFile = ioFile;
            UserDBPath = AppContext.BaseDirectory + USER_DB_PATH;
            IoFile.CreateDirectory(UserDBPath);
        }


        #endregion

        #region "Private Functions"

        /// <summary>
        /// Obtains File name from user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>User file name</returns>
        private string GetUserFileName(string userName)
        {
            return $@"{UserDBPath}\{userName.Trim()}.{USER_FILE_EXTENSION}";
        }

        #endregion

        #region "Protected Virtual Functions"

        protected virtual User ReadUserFromJsonFile(string fileName)
        {
            return IoFile.ReadFromJsonFile<User>(fileName);
        }

        #endregion

        #region "Public Functions"

        public User Get(string name)
        {
            string fileName = GetUserFileName(name);
            return IoFile.Exists(fileName) ? ReadUserFromJsonFile(fileName) : null;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            string[] userfiles = IoFile.GetDirectoryFilenames(UserDBPath, USER_FILE_EXTENSION);
            foreach (var fileName in userfiles)
            {
                users.Add(ReadUserFromJsonFile(fileName));
            }
            return users;
        }

        public bool Create(User UserData)
        {
            string fileName = GetUserFileName(UserData.UserName);
            if (!IoFile.Exists(fileName))
            {
                IoFile.WriteToJsonFile(UserData, fileName);
                return true;
            }
            return false;
        }

        public bool Update(User UserData)
        {
            string fileName = GetUserFileName(UserData.UserName);
            if (IoFile.Exists(fileName))
            {
                IoFile.WriteToJsonFile(UserData, fileName);
                return true;
            }
            return false;
        }

        public bool Delete(string name)
        {
            string fileName = GetUserFileName(name);
            if (IoFile.Exists(fileName))
            {
                IoFile.Delete(fileName);
                return true;
            }
            return false;
        }

        #endregion
    }
}