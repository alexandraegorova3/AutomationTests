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
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var percentage = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(By.XPath("//div[@id='fakhar-cricle']//div[@class='percenttext']"));
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

            if (percentage)
            {
                driver.Navigate().Refresh();
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
