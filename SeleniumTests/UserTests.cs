using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class UserTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
            driver.Url = "https://www.seleniumeasy.com/test/dynamic-data-loading-demo.html";
        }

        [Test]
        public void UserTest()
        {
            driver.FindElement(By.XPath("//button[text() = 'Get New User']")).Click();
            var user = Wait(By.XPath("//div[@id='loading'][contains(text(), 'First Name')]"), TimeSpan.FromSeconds(5));

            Assert.IsTrue(user);
        }

        public bool Wait(By locator, TimeSpan time)
        {
            var wait = new WebDriverWait(driver, time);

            return wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(locator);
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
