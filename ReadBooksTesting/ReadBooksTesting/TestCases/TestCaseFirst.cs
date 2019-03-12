using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniumTests
{
    [TestFixture]
    public class TestCaseFirst : BaseTestData
    {

        [Test]
        public void LogIn()
        {
            UserData user = new UserData() { Login = "12345", Password = "12345" };
            SignIn(user);
        }

        [Test]
        public void AddPost()
        {
            System.Drawing.Size windowSize = new System.Drawing.Size(2000, 800); driver.Manage().Window.Size = windowSize;
            LogIn();
            AddArticle("Какой то пост");
        }
    }
}
