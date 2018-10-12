using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectPattern
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement UserNameField
        {
            get { return this.driver.FindElement(By.Id("userName")); }
        }

        private IWebElement PasswordField
        {
            get { return this.driver.FindElement(By.Id("password")); }
        }

        private IWebElement LoginButton
        {
            get { return this.driver.FindElement(By.Id("loginBtn")); }
        }

        public HomePage Login(string userName, string password)
        {
            this.UserNameField.SendKeys(userName);
            this.PasswordField.SendKeys(password);
            this.LoginButton.Click();
            return new HomePage(this.driver);
        }
    }
}
