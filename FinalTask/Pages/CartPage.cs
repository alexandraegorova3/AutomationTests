using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class CartPage: BasePage
    {
        private By cartSummatyHeaderLocator = By.XPath("//h1[contains(text(),'Shopping-cart summary')]");
        private By totalLocator = By.XPath("//td[@id='total_product']");
        private By cartItemNameLocator = By.XPath("//tr[contains(@class, 'cart_item')]//p[@class='product-name']/a");
        private By deleteButtonLocator = By.XPath("//td[contains(@class, 'cart_delete')]//a");

        public CartPage(IWebDriver driver) : base(driver)
        {
            Wait(cartSummatyHeaderLocator, TimeSpan.FromSeconds(5));
        }

        public List<string> GetItemsInCart()
        {
            var itemNames = new List<string>();
            var items = driver.FindElements(cartItemNameLocator);
            foreach (var item in items)
            {
                itemNames.Add(item.Text);
            }

            return itemNames;
        }

        public double GetTotal()
        {
            return double.Parse(driver.FindElement(totalLocator).Text.Replace('$', ' ').Trim(), CultureInfo.InvariantCulture);
        }

        public void CleanCart()
        {
            var itemsToDelete = driver.FindElements(deleteButtonLocator);
            foreach (var item in itemsToDelete)
            {
                item.Click();
            }
        }
    }
}
