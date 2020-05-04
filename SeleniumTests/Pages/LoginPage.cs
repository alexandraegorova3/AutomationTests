using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class LoginPage: BaseTutByPage
    {
        private By enterButtonLocator = By.XPath("//a[@class = 'enter']");
        private By usernameLocator = By.XPath("//input[@name = 'login']");
        private By passwordLocator = By.XPath("//input[@name = 'password']");
        private By loginButtonLocator = By.XPath("//input[@type = 'submit']");

        public bool IsLoaded => Wait(enterButtonLocator, TimeSpan.FromSeconds(10));

        public LoginPage(IWebDriver driver): base(driver)
        {
        }

        public void Load()
        {
            driver.Url = "http://tut.by";
        }

        private void ClickEnter()
        {
            driver.FindElement(enterButtonLocator).Click();
        }

        private void TypeUsername(string username)
        {
            driver.FindElement(usernameLocator).SendKeys(username);
        }

        private void TypePassword(string password)
        {
            driver.FindElement(passwordLocator).SendKeys(password);
        }

        private MainPage SubmitLogin()
        {
            driver.FindElement(loginButtonLocator).SendKeys(Keys.Enter);
            return new MainPage(driver);
        }

        public MainPage LoginAs(string username, string password)
        {
            ClickEnter();
            TypeUsername(username);
            TypePassword(password);
            return SubmitLogin();
        }


    }
}
