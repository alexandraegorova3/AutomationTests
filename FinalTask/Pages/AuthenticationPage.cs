using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class AuthenticationPage: BasePage
    {
        private By createEmailLocator = By.XPath("//input[@id = 'email_create']");
        private By createAccountButton = By.XPath("//button[@id = 'SubmitCreate']");
        private By emailLocator = By.XPath("//input[@id = 'email']");
        private By passwordLocator = By.XPath("//input[@id = 'passwd']");
        private By signInButtonLocator = By.XPath("//button[@id = 'SubmitLogin']");
        private By authenticationHeader = By.XPath("//h1[text()='Authentication']");
        private By accountCreationHeader = By.XPath("//h1[text()='Create an account']");
        private string pageUrl = "http://automationpractice.com/index.php?controller=authentication&back=my-account";

        public AuthenticationPage(IWebDriver driver) : base(driver)
        {
            Wait(authenticationHeader, TimeSpan.FromSeconds(5));
        }

        public AccountCreationPage CreateAccount(string email)
        {
            driver.FindElement(createEmailLocator).SendKeys(email);
            driver.FindElement(createAccountButton).SendKeys(Keys.Enter);
            Wait(accountCreationHeader, TimeSpan.FromSeconds(5));

            return new AccountCreationPage(driver);
        }

        public MyAccountPage SignIn(string email, string password)
        {
            Wait(authenticationHeader, TimeSpan.FromSeconds(5));
            driver.FindElement(emailLocator).SendKeys(email);
            driver.FindElement(passwordLocator).SendKeys(password);
            driver.FindElement(signInButtonLocator).SendKeys(Keys.Enter);

            return new MyAccountPage(driver);
        }
    }
}
