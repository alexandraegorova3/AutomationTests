using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class MyAccountPage: BasePage
    {
        private By myAccountHeader = By.XPath("//h1[text()='My account']");
        private By myWishListsButton = By.XPath("//a[@title='My wishlists']");

        public bool IsLoaded;

        public MyAccountPage(IWebDriver driver): base(driver) 
        {
            IsLoaded = Wait(myAccountHeader, TimeSpan.FromSeconds(10));
        }

        public WishlistPage ToMyWishListPage()
        {
            driver.FindElement(myWishListsButton).Click();
            return new WishlistPage(driver);
        }
    }
}
