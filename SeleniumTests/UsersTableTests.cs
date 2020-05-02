using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace SeleniumTests
{
    public class UsersTableTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aleks\Downloads");
            driver.Url = "https://www.seleniumeasy.com/test/table-sort-search-demo.html";
        }

        [Test]
        public void UserTest()
        {
            IWebElement element = driver.FindElement(By.XPath("//select"));
            SelectElement select = new SelectElement(element);
            select.SelectByValue("100");
            var users = GetUsers(60, 100000);
        }

        public List<User> GetUsers(int age, int salary)
        {
            var rows = driver.FindElements(By.XPath("//table//tbody/tr"));
            var users = new List<User>();
            foreach (var row in rows)
            {
                var x = int.Parse(row.FindElement(By.XPath(".//td[position() = 4]")).Text);
                var salaryText = row.FindElement(By.XPath(".//td[position() = 6]")).Text;
                salaryText = salaryText.Replace('$', ' ');
                salaryText = salaryText.Replace('/', ' ');
                salaryText = salaryText.Replace('y', ' ');
                salaryText = string.Join("",salaryText.Split(','));
                var y = int.Parse(salaryText);
                if (x > age && y > salary)
                {
                    var name = row.FindElement(By.XPath(".//td[position() = 1]")).Text;
                    var position = row.FindElement(By.XPath(".//td[position() = 2]")).Text;
                    var office = row.FindElement(By.XPath(".//td[position() = 3]")).Text;
                    var user = new User
                    {
                        Name = name,
                        Position = position,
                        Office = office
                    };
                    users.Add(user);
                }
            }

            return users;
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }

    public class User
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public string Office { get; set; }
    }
}
