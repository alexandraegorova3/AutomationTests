using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumTests
{
    public class Test
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
            driver.Url = "http://tut.by";
        }

        [Test]
        public void Test1()
        {
            var findMethods = new FindMethods();
            findMethods.FindByXPath(driver, "//a[@class = 'enter']").Click();
            var loginInput = driver.FindElement(By.XPath("//input[@name = 'login']"));
            loginInput.SendKeys("aegorova@tut.by");

            var passwordInput = driver.FindElement(By.XPath("//input[@name = 'password']"));
            passwordInput.SendKeys("mtfbwy8772902");

            var submitButton = driver.FindElement(By.XPath("//input[@type = 'submit']"));
            submitButton.SendKeys(Keys.Enter);

            var success = driver.FindElement(By.XPath("//a[contains(@class,'logedin')]"));
            Assert.True(success.Displayed,"Success element is not displayed");
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }

    }
}
