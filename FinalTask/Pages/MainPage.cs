using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

namespace FinalTask.Pages
{
    public class MainPage: BasePage
    {
        private By signInButtonLocator = By.XPath("//a[@class = 'login']");
        private By signOutButtonLocator = By.XPath("//a[@class = 'logout']");
        private By pageDivlocator = By.XPath("//div[@id = 'page']");
        private By productItemLocator = By.XPath("//li[contains(@class, 'ajax_block_product')]");
        private By productNameLocator = By.XPath("//a[@class='product-name']");
        private By productPriceLocator = By.XPath("//div[@class='right-block']//span[contains(@class, 'product-price')]");
        private By addToCartButtonLocator = By.XPath("//a[contains(@class, 'ajax_add_to_cart_button')]");
        private By cartButtonLocator = By.XPath("//a[@title='View my shopping cart']");
        private By popupLocator = By.XPath("//div[@id = 'layer_cart']");
        private By popupCloseButtonLocator = By.XPath("//span[@title = 'Close window']");
        private string pageUrl = "http://automationpractice.com";

        public MainPage(IWebDriver driver): base(driver)
        {
        }

        public void Load()
        {
            driver.Url = pageUrl;
            Wait(pageDivlocator, TimeSpan.FromSeconds(5));
        }

        public AuthenticationPage ToSignInPage()
        {
            driver.FindElement(signInButtonLocator).SendKeys(Keys.Enter);
            return new AuthenticationPage(driver);
        }

        public Productitem[] AddItemsToCard(int itemsCount)
        {
            var productItems = new Productitem[itemsCount];
            var items = driver.FindElements(productNameLocator).ToArray();
            var itemsPrices = driver.FindElements(productPriceLocator).ToArray();
            var addToCartButtons = driver.FindElements(addToCartButtonLocator).ToArray();

            for (int i = 0; i < itemsCount; i++)
            {
                productItems[i] = new Productitem
                {
                    Name = items[i].Text,
                    Price = double.Parse(itemsPrices[i].Text.Replace('$', ' ').Trim(), CultureInfo.InvariantCulture)
                };
                Actions actions = new Actions(driver);
                driver.ExecuteJavaScript("arguments[0].scrollIntoView();", items[i]);
                actions.MoveToElement(items[i]).Perform();
                addToCartButtons[i].Click();
                Wait(popupLocator, TimeSpan.FromSeconds(5));
                driver.FindElement(popupCloseButtonLocator).Click();
            }

            return productItems;
        }

        public CartPage ToShoppingCart()
        {
            driver.FindElement(cartButtonLocator).Click();
            return new CartPage(driver);
        }

        public void SignOut()
        {
            driver.FindElement(signOutButtonLocator).Click();
            Wait(signInButtonLocator, TimeSpan.FromSeconds(5));
        }
    }
}
