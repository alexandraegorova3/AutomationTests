using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class WishlistPage: BasePage
    {
        private By wishListHistoryLocator = By.XPath("//div[@id = 'block-history']");
        private By myWishListsHeader = By.XPath("//h1[text()='My wishlists']");
        private By productDetailsLinkLocator = By.XPath("//div[@id='best-sellers_block_right']//a[contains(@class,'products-block-image')]");
        private By wishlistItemLocator = By.XPath("//td[@class='wishlist_delete']/a");
        private By wishlistNameLocator = By.XPath("//input[@id = 'name']");
        private By addWishListButtonLocator = By.XPath("//button[@id = 'submitWishlist']");
        private string pageUrl = "http://automationpractice.com/index.php?fc=module&module=blockwishlist&controller=mywishlist";
        
        public WishlistPage(IWebDriver driver) : base(driver)
        {
            Wait(myWishListsHeader, TimeSpan.FromSeconds(5));
            CleanUpWishList();
        }

        private void CleanUpWishList()
        {
            var wishlistitems = driver.FindElements(wishlistItemLocator);

            foreach (var wishlistitem in wishlistitems)
            {
                wishlistitem.Click();
                var alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
        }

        public ProductDetailsPage ToProductDetailsPage()
        {
            driver.FindElement(productDetailsLinkLocator).Click();
            return new ProductDetailsPage(driver);
        }

        public void Load() 
        {
            driver.Url = pageUrl;
        }

        public bool IsEmptyWishList() 
        {
            return driver.FindElements(wishListHistoryLocator).Count == 0;
        }

        public string WishListItemsCount(string wishListName)
        {
            var element = driver.FindElement(By.XPath($"//td//a[contains(text(), {wishListName})]/parent::td//following-sibling::td"));
            return element.Text;
        }

        public void DeleteFromWishlist() 
        {
            driver.FindElement(wishlistItemLocator).Click();
        }

        public void AddWishList(string wishListName)
        {
            driver.FindElement(wishlistNameLocator).SendKeys(wishListName);
            driver.FindElement(addWishListButtonLocator).Click();
        }
    }
}
