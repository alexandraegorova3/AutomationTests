using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
        }

        [Test]
        public void Test1()
        {
            driver.Url = "http://tut.by";
            driver.FindElement(By.XPath("//a[@class = 'enter']")).Click();
            var login = driver.FindElement(By.XPath("//input[@name = 'login']"));
            login.SendKeys("aegorova@tut.by");

            var password = driver.FindElement(By.XPath("//input[@name = 'password']"));
            password.SendKeys("mtfbwy8772902");

            var button = driver.FindElement(By.XPath("//input[@type = 'submit']"));
            button.SendKeys(Keys.Enter);

            var success = driver.FindElement(By.XPath("//a[contains(@class,'logedin')]"));
            Assert.True(success != null);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }

    }
}
