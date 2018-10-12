using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectPattern
{
    public class LoginPageFromFactory
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "userName")]
        private IWebElement userNameField;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordField;

        [FindsBy(How = How.Id, Using = "loginBtn")]
        private IWebElement loginButton;

        public LoginPageFromFactory(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }

        public HomePage Login(string userName, string password)
        {
            this.userNameField.SendKeys(userName);
            this.passwordField.SendKeys(password);
            this.loginButton.Click();
            return new HomePage(this.driver);
        }
    }
}
