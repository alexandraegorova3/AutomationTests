using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests
{
    public class AlertsTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
            driver.Url = "https://www.seleniumeasy.com/test/javascript-alert-box-demo.html";
        }

        [Test]
        public void AlertTest()
        {
            driver.FindElement(By.XPath("//div[text() = 'Java Script Alert Box']/following-sibling::div//button")).Click();
            var alert = driver.SwitchTo().Alert();
            Assert.NotNull(alert);
            alert.Accept();
        }

        [Test]
        public void ConfitmTest()
        {
            driver.FindElement(By.XPath("//div[text() = 'Java Script Confirm Box']/following-sibling::div//button")).Click();
            var alert = driver.SwitchTo().Alert();
            alert.Accept();
            var confirm = driver.FindElement(By.Id("confirm-demo"));
            Assert.AreEqual(confirm.Text, "You pressed OK!");
        }

        [Test]
        public void DismissTest()
        {
            driver.FindElement(By.XPath("//div[text() = 'Java Script Confirm Box']/following-sibling::div//button")).Click();
            var alert = driver.SwitchTo().Alert();
            alert.Dismiss();
            var confirm = driver.FindElement(By.Id("confirm-demo"));
            Assert.AreEqual(confirm.Text, "You pressed Cancel!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
