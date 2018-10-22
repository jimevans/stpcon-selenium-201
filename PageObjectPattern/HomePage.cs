// <copyright file="HomePage.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace PageObjectPatternExamples
{
    using System;
    using OpenQA.Selenium;

    /// <summary>
    /// A class that represents the application's home page after logging in.
    /// </summary>
    public class HomePage : BasePage
    {
        /// <summary>
        /// Creates a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> object used to automate the page.</param>
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Gets the confirmation text that the user has logged into the secure area.
        /// </summary>
        public string ConfirmationText
        {
            get
            {
                IWebElement element = this.GetElement(By.CssSelector("h2"), TimeSpan.FromSeconds(30));
                return element.Text;
            }
        }
    }
}