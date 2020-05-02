using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests
{
    public class ProgressTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
            driver.Url = "https://www.seleniumeasy.com/test/bootstrap-download-progress-demo.html";
        }

        [Test]
        public void UserTest()
        {
            driver.FindElement(By.Id("cricle-btn")).Click();
            var percentage = Wait(By.XPath("//div[@id='fakhar-cricle']//div[@class='percenttext']"), TimeSpan.FromSeconds(20));

            if (percentage)
            {
                driver.Navigate().Refresh();
            }
        }

        public bool Wait(By locator, TimeSpan time)
        {
            var wait = new WebDriverWait(driver, time);

            return wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(locator);
                    var x = int.Parse(elementToBeDisplayed.Text.Replace('%', ' '));
                    return x > 50;
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
