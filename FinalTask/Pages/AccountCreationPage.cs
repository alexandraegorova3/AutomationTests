using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FinalTask.Pages
{
    public class AccountCreationPage: BasePage
    {
        private By maleGenderLocator = By.XPath("//imput[@id = 'id_gender1']");
        private By femaleGenderLocator = By.XPath("//input[@id = 'id_gender2']");
        private By firstNameLocator = By.XPath("//input[@id = 'customer_firstname']");
        private By lastNameLocator = By.XPath("//input[@id = 'customer_lastname']");
        private By passwordLocator = By.XPath("//input[@id = 'passwd']");

        private By addressFirstNameLocator = By.XPath("//input[@id = 'firstname']");
        private By addressLastnameLocator = By.XPath("//input[@id = 'lastname']");
        private By addressLocator = By.XPath("//input[@id = 'address1']");
        private By cityLocator = By.XPath("//input[@id = 'city']");
        private By stateLocator = By.XPath("//select[@id = 'id_state']");
        private By zipCodeLocator = By.XPath("//input[@id = 'postcode']");
        private By mobilePhoneLocator = By.XPath("//input[@id = 'phone_mobile']");
        private By registerButtonLocator = By.XPath("//button[@id = 'submitAccount']");


        public AccountCreationPage(IWebDriver driver) : base(driver)
        {
        }

        public MyAccountPage CreateAccount(Gender gender, string firstName, string lastName, string password, string address, string city, string state, string zip, string mobilePhone)
        {
            if(gender == Gender.Male)
            {
                driver.FindElement(maleGenderLocator).Click();
            } 
            else
            {
                driver.FindElement(femaleGenderLocator).Click();
            }
            driver.FindElement(firstNameLocator).SendKeys(firstName);
            driver.FindElement(lastNameLocator).SendKeys(lastName);
            driver.FindElement(passwordLocator).SendKeys(password);

            driver.FindElement(addressFirstNameLocator).SendKeys(firstName);
            driver.FindElement(addressLastnameLocator).SendKeys(lastName);
            driver.FindElement(addressLocator).SendKeys(address);
            driver.FindElement(cityLocator).SendKeys(city);
            var element = driver.FindElement(stateLocator);
            new SelectElement(element).SelectByText(state);
            driver.FindElement(zipCodeLocator).SendKeys(zip);
            driver.FindElement(mobilePhoneLocator).SendKeys(mobilePhone);
            driver.FindElement(registerButtonLocator).Click();

            return new MyAccountPage(driver);
        }
    }
}
