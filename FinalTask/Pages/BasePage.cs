using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FinalTask.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        protected bool Wait(By locator, TimeSpan time)
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
