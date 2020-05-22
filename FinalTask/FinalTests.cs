using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using Allure.Commons;
using Allure.Commons.Model;
using Allure.NUnit.Attributes;
using FinalTask.Pages;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace FinalTask
{
    public enum RunConfiguration
    {
        Local,
        Grid,
        Cloud
    }

    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class FinalTests<TWebDriver>: AllureReport where TWebDriver : IWebDriver, new()
    {
        IWebDriver driver;
        RunConfiguration configuration = RunConfiguration.Local;
        MainPage mainPage;

        [OneTimeSetUp]
        public void Init(){
            switch (configuration)
            {
                case RunConfiguration.Local:
                {
                        driver = new TWebDriver();
                }
                break;
                case RunConfiguration.Grid:
                {
                        if (typeof(TWebDriver) == typeof(ChromeDriver))
                        {
                            var capabilities = new ChromeOptions();
                            driver = new RemoteWebDriver(new Uri("http://192.168.96.1:4445/wd/hub"), capabilities);
                        } else {
                            var capabilities = new FirefoxOptions();
                            driver = new RemoteWebDriver(new Uri("http://192.168.96.1:4445/wd/hub"), capabilities);
                        }
                }
                break;
                case RunConfiguration.Cloud:
                {
                    if (typeof(TWebDriver) == typeof(ChromeDriver))
                    {
                        var capabilities = new ChromeOptions();
                            driver = new RemoteWebDriver(new Uri("https://alexandraegorova:692364a6-3e80-434f-bc0a-748480390724@ondemand.eu-central-1.saucelabs.com:443/wd/hub"),
                            capabilities);
                        }
                    else
                    {
                            var sauceOptions = new Dictionary<string, object>();
                            var capabilities = new FirefoxOptions();
                            capabilities.PlatformName = "Windows 10";
                            capabilities.BrowserVersion = "76.0";
                            capabilities.AddAdditionalCapability("sauce:options", sauceOptions, true);
                            driver = new RemoteWebDriver(new Uri("https://alexandraegorova:692364a6-3e80-434f-bc0a-748480390724@ondemand.eu-central-1.saucelabs.com:443/wd/hub"), capabilities);
                    }
                }
                break;
            }
            
        }

        [SetUp]
        public void Setup()
        {
            mainPage = new MainPage(driver);
            mainPage.Load();
        }


        [Test]
        [AllureSubSuite("Final task")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("http://automationpractice.com/")]
        [AllureTest("Create account test")]
        [AllureOwner("Alexandra Egorova")]
        public void CreateAccount()
        {
            var random = new Random();
            var email = random.Next(1000) + "@test.test";
            var authenticationPage = mainPage.ToSignInPage();
            var accountCreationPage = authenticationPage.CreateAccount(email);
            var myAccountPage = accountCreationPage.CreateAccount(Gender.Female, "Test", "User", "123456", "Some address", "Some city", "Alabama", "12345", "121345678");

            Assert.True(myAccountPage.IsLoaded, "User is not registered");
        }

        [Test]
        [AllureSubSuite("Final task")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("http://automationpractice.com/")]
        [AllureTest("Login test")]
        [AllureOwner("Alexandra Egorova")]
        public void Login()
        {
            var authenticationPage = mainPage.ToSignInPage();
            var myAccountpage = authenticationPage.SignIn("123@test.test", "123456");

            Assert.True(myAccountpage.IsLoaded, "User is not logged in");
        }

        [Test]
        [AllureSubSuite("Final task")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("http://automationpractice.com/")]
        [AllureTest("Auto wish list test")]
        [AllureOwner("Alexandra Egorova")]
        public void AutoWishList()
        {
            var authenticationPage = mainPage.ToSignInPage();
            var myAccountpage = authenticationPage.SignIn("123@test.test", "123456");
            var wishListPage = myAccountpage.ToMyWishListPage();

            var productDetailsPage = wishListPage.ToProductDetailsPage();
            productDetailsPage.AddToWishList();
            wishListPage.Load();

            Assert.True(!wishListPage.IsEmptyWishList(), "Wishlist is empty");
        }

        [Test]
        [AllureSubSuite("Final task")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("http://automationpractice.com/")]
        [AllureTest("Own wish list test")]
        [AllureOwner("Alexandra Egorova")]
        public void OwnWishList()
        {
            var authenticationPage = mainPage.ToSignInPage();
            var myAccountpage = authenticationPage.SignIn("123@test.test", "123456");
            var wishListPage = myAccountpage.ToMyWishListPage();
            wishListPage.AddWishList("wishlist");

            var productDetailsPage = wishListPage.ToProductDetailsPage();
            productDetailsPage.AddToWishList();
            wishListPage.Load();

            Assert.True(wishListPage.WishListItemsCount("wishlist") == "1", "Product was not added to a wishlist");
        }

        [Test]
        [AllureSubSuite("Final task")]
        [AllureSeverity(SeverityLevel.Critical)]
        [AllureLink("http://automationpractice.com/")]
        [AllureTest("Add to cart test")]
        [AllureOwner("Alexandra Egorova")]
        public void Cart()
        {
            var authenticationPage = mainPage.ToSignInPage();
            authenticationPage.SignIn("123@test.test", "123456");
            mainPage.Load();
            var cartPage = mainPage.ToShoppingCart();
            cartPage.CleanCart();
            mainPage.Load();
            var selectedItems = mainPage.AddItemsToCard(3);
            cartPage = mainPage.ToShoppingCart();
            var itemsInCart = cartPage.GetItemsInCart();

            Assert.True(selectedItems.Length == itemsInCart.Count, "Not all items were added to cart");

            double selectedItemsTotal = 0;
            foreach (var item in selectedItems)
            {
                Assert.True(itemsInCart.Contains(item.Name), "Item was not added to cart");
                selectedItemsTotal = selectedItemsTotal + item.Price;
            }

            var itemsInCartTotal = cartPage.GetTotal();

            Assert.True(selectedItemsTotal == itemsInCartTotal, "Totals do not match");
        }

        [TearDown]
        public void HandleError()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile(@"C:\Users\aleks\Downloads\Failed.png", ScreenshotImageFormat.Png);
                AllureLifecycle.Instance.AddAttachment($"{TestContext.CurrentContext.Test.FullName}-{DateTime.Now}.png", AllureLifecycle.AttachFormat.ImagePng, image.AsByteArray, "png");
                var info = JObject.FromObject(new
                {
                    date = DateTime.Now,
                    browser = typeof(TWebDriver) == typeof(FirefoxDriver) ? "Firefor" : "Chrome",
                    platform = Environment.OSVersion.ToString()
                });
                AllureLifecycle.Instance.AddAttachment($"info.json", AllureLifecycle.AttachFormat.Json, Encoding.ASCII.GetBytes(info.ToString()));
            }
        }

        [TearDown]
        public void Logout()
        {
            mainPage.SignOut();
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver?.Quit();
        }
    }
}
