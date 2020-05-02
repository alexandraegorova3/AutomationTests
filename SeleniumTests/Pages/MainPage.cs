using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class MainPage
    {
        private IWebDriver driver;
        private By userNameLocator = By.XPath("//span[@class='uname']");
        private By logoutButtonLocator = By.LinkText("Выйти");

        public bool IsLoaded;

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            IsLoaded = Wait(userNameLocator, TimeSpan.FromSeconds(10));
        }

        public LoginPage Logout()
        {
            driver.FindElement(userNameLocator).Click();
            driver.FindElement(logoutButtonLocator).Click();
            return new LoginPage(driver);
        }

        private bool Wait(By locator, TimeSpan time)
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
    }
}
