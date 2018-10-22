// <copyright file="LoginPageFromFactory.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace PageObjectPatternExamples
{
    using OpenQA.Selenium;
    using SeleniumExtras.PageObjects;

    /// <summary>
    /// Represents the login page for the web application, using the <see cref="PageFactory"/>.
    /// </summary>
    public class LoginPageFromFactory
    {
        /// <summary>
        /// The <see cref="IWebDriver"/> object used to automate the page.
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Gets the user name element. Note the field is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userNameField;

        /// <summary>
        /// Gets the password element. Note the field is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordField;

        /// <summary>
        /// Gets the login button element. Note the field is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "button")]
        private IWebElement loginButton;

        /// <summary>
        /// Gets the error message element. Note the field is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        [FindsBy(How = How.Id, Using = "flash")]
        private IWebElement errorMessage;

        /// <summary>
        /// Creates a new instance of the <see cref="LoginPageFromFactory"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> object used to automate the page.</param>
        public LoginPageFromFactory(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }

        /// <summary>
        /// Gets the error message displayed on a failed login attempt.
        /// </summary>
        public string LoginErrorMessage
        {
            get { return this.errorMessage.Text; }
        }

        /// <summary>
        /// Logs into the application.
        /// </summary>
        /// <param name="userName">The user name to use when logging in.</param>
        /// <param name="password">The password to use when logging in.</param>
        /// <returns>The correctly logged in <see cref="HomePage"/>.</returns>
        public HomePage Login(string userName, string password)
        {
            this.userNameField.SendKeys(userName);
            this.passwordField.SendKeys(password);
            this.loginButton.Click();
            return new HomePage(this.driver);
        }

        /// <summary>
        /// Attempts to log into the application with an invalid user name.
        /// </summary>
        /// <param name="userName">The user name to use when attempting to logging in.</param>
        /// <returns>This <see cref="LoginPageFromFactory"/>.</returns>
        public LoginPageFromFactory LoginWithInvalidUserName(string userName)
        {
            this.userNameField.SendKeys(userName);
            this.loginButton.Click();
            return this;
        }

        /// <summary>
        /// Attempts to log into the application with an invalid password.
        /// </summary>
        /// <param name="userName">The user name to use when attempting to logging in.</param>
        /// <param name="password">The password to use when attempting to logging in.</param>
        /// <returns>This <see cref="LoginPageFromFactory"/>.</returns>
        public LoginPageFromFactory LoginWithInvalidPassword(string userName, string password)
        {
            this.userNameField.SendKeys(userName);
            this.passwordField.SendKeys(password);
            this.loginButton.Click();
            return this;
        }
    }
}
