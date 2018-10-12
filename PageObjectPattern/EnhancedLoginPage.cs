using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectPattern
{
    public class EnhancedLoginPage : BasePage
    {
        public EnhancedLoginPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement UserNameField
        {
            get { return this.GetElement(By.Id("userName"), TimeSpan.FromSeconds(30)); }
        }

        private IWebElement PasswordField
        {
            get { return this.GetElement(By.Id("password")); }
        }

        private IWebElement LoginButton
        {
            get { return this.GetElement(By.Id("loginBtn")); }
        }

        public HomePage Login(string userName, string password)
        {
            this.UserNameField.SendKeys(userName);
            this.PasswordField.SendKeys(password);
            this.LoginButton.Click();
            return new HomePage(this.Driver);
        }
    }
}
