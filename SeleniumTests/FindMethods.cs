using OpenQA.Selenium;

namespace SeleniumTests
{
    public class FindMethods
    {
        public IWebElement FindByXPath(IWebDriver driver, string xpath)
        {
            return driver.FindElement(By.XPath(xpath));
        }

        public IWebElement FindByClassName(IWebDriver driver, string className)
        {
            return driver.FindElement(By.ClassName(className));
        }

        public IWebElement FindByCssSelector(IWebDriver driver, string selector)
        {
            return driver.FindElement(By.CssSelector(selector));
        }

        public IWebElement FindById(IWebDriver driver, string id)
        {
            return driver.FindElement(By.Id(id));
        }
        public IWebElement FindByName(IWebDriver driver, string name)
        {
            return driver.FindElement(By.Name(name));
        }
        public IWebElement FindByLinkText(IWebDriver driver, string linkText)
        {
            return driver.FindElement(By.LinkText(linkText));
        }
        public IWebElement FindByPartialLinkText(IWebDriver driver, string partialLinkText)
        {
            return driver.FindElement(By.PartialLinkText(partialLinkText));
        }
        public IWebElement FindByTagName(IWebDriver driver, string tagName)
        {
            return driver.FindElement(By.TagName(tagName));
        }
    }

}
