// <copyright file="PageObjectPatternExamples.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace PageObjectPatternExamples
{
    using System;
    using OpenQA.Selenium;
    using WebDriverExampleUtilities;

    /// <summary>
    /// Class containing sample code to demonstrate use of a page objects with WebDriver. 
    /// </summary>
    class PageObjectPatternExamples
    {
        private BrowserKind browserKind;
        private string baseUrl = string.Empty;
        private IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageObjectPatternExamples"/> class.
        /// </summary>
        /// <param name="browserKind">The <see cref="BrowserKind"/> value to use to browser the pages.</param>
        /// <param name="baseUrl">The base URL used to navigate to the pages.</param>
        public PageObjectPatternExamples(BrowserKind browserKind, string baseUrl)
        {
            this.browserKind = browserKind;
            this.baseUrl = baseUrl;
        }

        /// <summary>
        /// Sets up the test, by creating a browser instance and navigating to the proper URL.
        /// </summary>
        public void SetUp()
        {
            this.driver = WebDriverFactory.CreateWebDriver(this.browserKind);
            this.driver.Url = this.baseUrl + "login";
        }

        /// <summary>
        /// Tears down the test, quitting the browser instance.
        /// </summary>
        public void TearDown()
        {
            this.driver.Quit();
        }

        /// <summary>
        /// Tests a successful login to the page.
        /// </summary>
        public void TestSuccessfulLogin()
        {
            this.SetUp();

            Console.WriteLine("Attempting correct login");
            LoginPageFromFactory loginPage = new LoginPageFromFactory(driver);
            HomePage home = loginPage.Login("tomsmith", "SuperSecretPassword!");
            if (home.ConfirmationText == "Secure Area")
            {
                Console.WriteLine("Logged in successfully!");
            }

            this.TearDown();
        }

        /// <summary>
        /// Tests an unsuccessful login to the page using an invalid user name.
        /// </summary>
        public void TestUnsuccessfulUserNameLogin()
        {
            this.SetUp();

            Console.WriteLine("Attempting login with incorrect user name");
            LoginPageFromFactory loginPage = new LoginPageFromFactory(driver);
            LoginPageFromFactory failedLoginPage = loginPage.LoginWithInvalidUserName("samjones");
            if (failedLoginPage.LoginErrorMessage.Contains("username"))
            {
                Console.WriteLine("Successfully detected incorrect user name!");
            }

            this.TearDown();
        }

        /// <summary>
        /// Tests an unsuccessful login to the page using an invalid password.
        /// </summary>
        public void TestUnsuccessfulPasswordLogin()
        {
            this.SetUp();

            Console.WriteLine("Attempting login with incorrect password");
            LoginPageFromFactory loginPage = new LoginPageFromFactory(driver);
            LoginPageFromFactory failedLoginPage = loginPage.LoginWithInvalidPassword("tomsmith", "foo");
            if (failedLoginPage.LoginErrorMessage.Contains("password"))
            {
                Console.WriteLine("Successfully detected incorrect password!");
            }

            this.TearDown();
        }
    }
}
