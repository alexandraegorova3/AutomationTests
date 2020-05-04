using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class MainPage: BaseTutByPage
    {
        private By userNameLocator = By.XPath("//span[@class='uname']");
        private By logoutButtonLocator = By.LinkText("Выйти");

        public bool IsLoaded;

        public MainPage(IWebDriver driver): base(driver)
        {
            IsLoaded = Wait(userNameLocator, TimeSpan.FromSeconds(10));
        }

        public LoginPage Logout()
        {
            driver.FindElement(userNameLocator).Click();
            driver.FindElement(logoutButtonLocator).Click();
            return new LoginPage(driver);
        }
    }
}
