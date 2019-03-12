using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }

    public class BaseTestData
    {

        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/testsite/wp-admin/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        protected void SignIn(UserData User)
        {
            driver.Navigate().GoToUrl("http://localhost/testsite/wp-login.php");
            LoginFieldInput(User);
            PasswordFIeldInput(User);
            driver.FindElement(By.Id("wp-submit")).Click();
        }

        protected void PasswordFIeldInput(UserData User)
        {
            driver.FindElement(By.Id("user_pass")).Click();
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys(User.Password);
        }

        protected void LoginFieldInput(UserData User)
        {
            driver.FindElement(By.Id("user_login")).Click();
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys(User.Login);
        }

        protected void AddArticle(string articleName)
        {
            driver.Navigate().GoToUrl("http://localhost/testsite/wp-admin/index.php");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Главная'])[1]/following::div[5]")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Записи'])[3]/following::a[1]")).Click();
            if (driver.FindElement(By.ClassName("nux-dot-tip__disable")).Displayed == true)
            {
                driver.FindElement(By.ClassName("nux-dot-tip__disable")).Click();
            }  
            driver.FindElement(By.Id("post-title-0")).Click();
            driver.FindElement(By.Id("post-title-0")).Clear();
            driver.FindElement(By.Id("post-title-0")).SendKeys(articleName);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='(откроется в новой вкладке)'])[1]/following::button[1]")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Начните писать или нажмите / для выбора блока'])[1]/following::button[2]")).Click();
            driver.FindElement(By.LinkText("Просмотреть запись"),10).Click();
             
        }



    }
}
