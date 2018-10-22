// <copyright file="LoginPage.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace PageObjectPatternExamples
{
    using OpenQA.Selenium;

    public class LoginPage
    {
        /// <summary>
        /// The <see cref="IWebDriver"/> object used to automate the page.
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Creates a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> object used to automate the page.</param>
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets the user name element. Note the property is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        private IWebElement UserNameField
        {
            get { return this.driver.FindElement(By.Id("username")); }
        }

        /// <summary>
        /// Gets the password element. Note the property is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        private IWebElement PasswordField
        {
            get { return this.driver.FindElement(By.Id("password")); }
        }

        /// <summary>
        /// Gets the login button element. Note the property is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        private IWebElement LoginButton
        {
            get { return this.driver.FindElement(By.CssSelector("button")); }
        }

        /// <summary>
        /// Gets the error message element. Note the property is intentionally
        /// private, and inaccessable outside this class.
        /// </summary>
        private IWebElement ErrorMessageDisplay
        {
            get { return this.driver.FindElement(By.Id("flash")); }
        }

        /// <summary>
        /// Gets the error message displayed on a failed login attempt.
        /// </summary>
        public string LoginErrorMessage
        {
            get { return this.ErrorMessageDisplay.Text; }
        }

        /// <summary>
        /// Logs into the application.
        /// </summary>
        /// <param name="userName">The user name to use when logging in.</param>
        /// <param name="password">The password to use when logging in.</param>
        /// <returns>The correctly logged in <see cref="HomePage"/>.</returns>
        public HomePage Login(string userName, string password)
        {
            this.UserNameField.SendKeys(userName);
            this.PasswordField.SendKeys(password);
            this.LoginButton.Click();
            return new HomePage(this.driver);
        }

        /// <summary>
        /// Attempts to log into the application with an invalid user name.
        /// </summary>
        /// <param name="userName">The user name to use when attempting to logging in.</param>
        /// <returns>This <see cref="LoginPage"/>.</returns>
        public LoginPage LoginWithInvalidUserName(string userName)
        {
            this.UserNameField.SendKeys(userName);
            this.LoginButton.Click();
            return this;
        }

        /// <summary>
        /// Attempts to log into the application with an invalid password.
        /// </summary>
        /// <param name="userName">The user name to use when attempting to logging in.</param>
        /// <param name="password">The password to use when attempting to logging in.</param>
        /// <returns>This <see cref="LoginPage"/>.</returns>
        public LoginPage LoginWithInvalidPassword(string userName, string password)
        {
            this.UserNameField.SendKeys(userName);
            this.PasswordField.SendKeys(password);
            this.LoginButton.Click();
            return this;
        }
    }
}
