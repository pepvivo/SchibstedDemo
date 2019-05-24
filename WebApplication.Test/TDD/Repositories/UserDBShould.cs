using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using WebApplication.Controllers;
using WebApplication.Modules;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.Repositories;
using Assert = NUnit.Framework.Assert;

namespace WebApplication.Test.TDD.Repositories
{
    /// <summary>
    ///  This test class uses inheritance of tested class 
    ///  this Test method overrrides concrete functions for testing purposes
    ///  It doesn't use Mock classes
    /// </summary>
    [TestFixture]
    public class UserDBShould
    {
        private static UserDBTestable UserDB { get; set; }
        private static string USER_NAME { get; set; }
        private static string USER_PSW { get; set; }
        public static User USER { get; set; }

        [SetUp]
        public void Init()
        {

            USER_NAME = "USER_NAME";
            USER_PSW = "USER_PSW";

            USER = new User()
            {
                UserName = USER_NAME,
                Password = USER_PSW
            };


        }

        [Test]
        public void Get_Should_return_an_user()
        {
            User userExpected = USER;

            UserDB = new UserDBTestable(new FakeIOFile());
            User user = UserDB.Get(USER_NAME);
            Assert.AreEqual(user, userExpected);
        }

        [Test]
        public void GetAll_Should_return_a_list_of_users()
        {
            List<User> usersExpected = new List<User>
            {
                USER,
                USER
            };

            UserDB = new UserDBTestable(new FakeIOFile());
            List<User> users = UserDB.GetAll();
            Assert.AreEqual(users, usersExpected);
        }

        [Test]
        public void Create_Should_return_true_when_create_new_user()
        {
            bool resultExpected = true;

            var fakeIOFile = new FakeIOFile();
            fakeIOFile.SetExists(false);
            UserDB = new UserDBTestable(fakeIOFile);

            var result = UserDB.Create(USER);
            Assert.AreEqual(result, resultExpected);
        }

        [Test]
        public void Update_Should_return_true_when_update_an_user()
        {
            bool resultExpected = true;

            UserDB = new UserDBTestable(new FakeIOFile());

            var result = UserDB.Update(USER);
            Assert.AreEqual(result, resultExpected);
        }

        [Test]
        public void Delete_Should_return_true_when_delete_an_user()
        {
            bool resultExpected = true;

            UserDB = new UserDBTestable(new FakeIOFile());

            var result = UserDB.Delete(USER.UserName);
            Assert.AreEqual(result, resultExpected);
        }

        /// <summary>
        /// Fake IOFIle class for testing Purposes
        /// </summary>
        internal class FakeIOFile : IIOFile
        {
            bool _exists = true;

            public void SetExists(bool exists)
            {
                _exists = exists;
            }
            
            public void CreateDirectory(string directoryPath)
            {
                return;
            }

            public void Delete(string fileName)
            {
                return;
            }

            public bool Exists(string fileName)
            {
                return _exists;
            }

            public string[] GetDirectoryFilenames(string filePath, string fileExtension, SearchOption searchOption = SearchOption.TopDirectoryOnly)
            {
                return new string[2] { string.Empty, string.Empty };
            }

            public T ReadFromJsonFile<T>(string fileName) 
            {
                return default(T);
            }

            public void WriteToJsonFile<T>(T obj, string fileName)
            {
                return;
            }

        }

        class UserDBTestable : UserDB
        {
            public UserDBTestable(IIOFile ioFile) : base(ioFile)
            {
            }

            protected override User ReadUserFromJsonFile(string fileName)
            {
                return USER;
            }

        }


    }
}
