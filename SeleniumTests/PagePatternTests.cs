using Allure.Commons;
using Allure.Commons.Model;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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
    public class PagePatternTests: AllureReport
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
        }

        [Test]
        [TestCase("seleniumtests@tut.by", "123456789zxcvbn")]
        [AllureSubSuite("Tut.by")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("https://www.tut.by/")]
        [AllureTest("Login test")]
        [AllureOwner("Alexandra Egorova")]
        public void LoginTest(string login, string password)
        {
            var loginPage = new LoginPage(driver);
            loginPage.Load();
            var mainPage = loginPage.LoginAs(login, password);
            Assert.True(mainPage.IsLoaded);
            var image = ((ITakesScreenshot)driver).GetScreenshot();
            image.SaveAsFile(@"C:\Users\aleks\Downloads\Login.png", ScreenshotImageFormat.Png);
        }

        [Test]
        [TestCase("seleniumtests@tut.by", "123456789zxcvbn")]
        [AllureSubSuite("Tut.by")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("https://www.tut.by/")]
        [AllureTest("Logout test")]
        [AllureOwner("Alexandra Egorova")]
        public void LogoutTest(string login, string password)
        {
            var loginPage = new LoginPage(driver);
            loginPage.Load();
            var mainPage = loginPage.LoginAs(login, password);
            loginPage = mainPage.Logout();
            Assert.True(loginPage.IsLoaded);
            var image = ((ITakesScreenshot)driver).GetScreenshot();
            image.SaveAsFile(@"C:\Users\aleks\Downloads\Logout.png", ScreenshotImageFormat.Png);
        }

        [TearDown]
        public void CloseBrowser()
        {
            if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile(@"C:\Users\aleks\Downloads\Failed.png", ScreenshotImageFormat.Png);
            }

            driver.Close();
        }
    }
}
