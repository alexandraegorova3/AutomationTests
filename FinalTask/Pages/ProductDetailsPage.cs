using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class ProductDetailsPage: BasePage
    {
        private By buyBlockLocator = By.XPath("//form[@id = 'buy_block']");
        private By addToWishListButtonLocator = By.XPath("//a[@id = 'wishlist_button']");
        private By popupCloseButtonLocator = By.XPath("//a[contains(@class, 'fancybox-close')]");

        public ProductDetailsPage(IWebDriver driver) : base(driver)
        {
            Wait(buyBlockLocator, TimeSpan.FromSeconds(5));
        }

        public void AddToWishList() 
        {
            driver.FindElement(addToWishListButtonLocator).Click();
            Wait(popupCloseButtonLocator, TimeSpan.FromSeconds(5));
            driver.FindElement(popupCloseButtonLocator).Click();
        }
    }
}
