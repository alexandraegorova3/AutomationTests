using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class MultiselectTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
            driver.Url = "https://www.seleniumeasy.com/test/basic-select-dropdown-demo.html";
        }

        [Test]
        public void MultiselectTest()
        {
            IWebElement element = driver.FindElement(By.Id("multi-select"));
            SelectElement select = new SelectElement(element);
            select.SelectByText("Florida");
            select.SelectByText("California");
            select.SelectByText("Texas");
            Assert.AreEqual(select.AllSelectedOptions.Count, 3);
            
         }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
