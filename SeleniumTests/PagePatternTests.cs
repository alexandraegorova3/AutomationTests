using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class PagePatternTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
        }

        [Test]
        [TestCase("seleniumtests@tut.by", "123456789zxcvbn")]
        public void LoginTest(string login, string password)
        {
            var loginPage = new LoginPage(driver);
            loginPage.Load();
            var mainPage = loginPage.LoginAs(login, password);
            Assert.True(mainPage.IsLoaded);
        }

        [Test]
        [TestCase("seleniumtests@tut.by", "123456789zxcvbn")]
        public void LogoutTest(string login, string password)
        {
            var loginPage = new LoginPage(driver);
            loginPage.Load();
            var mainPage = loginPage.LoginAs(login, password);
            loginPage = mainPage.Logout();
            Assert.True(loginPage.IsLoaded);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
