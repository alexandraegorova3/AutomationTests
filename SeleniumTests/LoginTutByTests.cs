using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Reflection.Emit;
using System.Threading;

namespace SeleniumTests
{
    public class LoginTutByTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Url = "http://tut.by";
        }

        [Test]
        [TestCase("seleniumtests@tut.by", "123456789zxcvbn")]
        [TestCase("seleniumtests2@tut.by", "123456789zxcvbn")]
        public void LoginTest(string login, string password)
        {
            var findMethods = new FindMethods();
            findMethods.FindByXPath(driver, "//a[@class = 'enter']").Click();
            var loginInput = driver.FindElement(By.XPath("//input[@name = 'login']"));
            loginInput.SendKeys(login);

            var passwordInput = driver.FindElement(By.XPath("//input[@name = 'password']"));
            passwordInput.SendKeys(password);

            var submitButton = driver.FindElement(By.XPath("//input[@type = 'submit']"));
            submitButton.SendKeys(Keys.Enter);

            Thread.Sleep(2000); //explicit wait

            var logedInSection = driver.FindElement(By.XPath("//a[contains(@class,'logedin')]"));
            Assert.True(logedInSection.Displayed,"Success element is not displayed");
        }

        [Test]
        [TestCase("seleniumtests@tut.by", "123456789zxcvbn")]
        [TestCase("seleniumtests2@tut.by", "123456789zxcvbn")]
        public void LoginTestWithExplicitWaiter(string login, string password)
        {
            driver.FindElement(By.XPath("//a[@class = 'enter']")).Click();
            var loginInput = driver.FindElement(By.XPath("//input[@name = 'login']"));
            loginInput.SendKeys(login);

            var passwordInput = driver.FindElement(By.XPath("//input[@name = 'password']"));
            passwordInput.SendKeys(password);

            var submitButton = driver.FindElement(By.XPath("//input[@type = 'submit']"));
            submitButton.SendKeys(Keys.Enter);


            var userName = Wait(By.XPath("//span[@class='uname']"), TimeSpan.FromSeconds(15));
            

            Assert.True(userName, "User name is not displayed");
        }

        public bool Wait(By locator, TimeSpan time)
        {
            var wait = new WebDriverWait(driver, time);

            return  wait.Until(condition =>
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
