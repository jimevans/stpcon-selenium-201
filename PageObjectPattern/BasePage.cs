// <copyright file="BasePage.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace PageObjectPatternExamples
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Represents the base page for a Page Object Pattern library.
    /// </summary>
    public class BasePage
    {
        /// <summary>
        /// The <see cref="IWebDriver"/> object used to automate the page.
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> object driving the pages.</param>
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets a reference to the current <see cref="IWebDriver"/> object.
        /// </summary>
        protected IWebDriver Driver
        {
            get { return this.driver; }
        }

        /// <summary>
        /// Gets an element based on the specified locator.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator to use to find the element.</param>
        /// <returns>The <see cref="IWebElement"/> found by the locator.</returns>
        /// <exception cref="NoSuchElementException">Thrown if the element specified 
        /// by the locator does not exist in the page.</exception>
        protected IWebElement GetElement(By locator)
        {
            return this.driver.FindElement(locator);
        }

        /// <summary>
        /// Gets an element based on the specified locator, within the specified timeout.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator to use to find the element.</param>
        /// <param name="timeout">The <see cref="TimeSpan"/> representing the amount of time
        /// to look for the element.</param>
        /// <returns>The <see cref="IWebElement"/> found by the locator.</returns>
        /// <exception cref="WebDriverTimeoutException">Thrown if the element specified 
        /// by the locator does not exist in the page within the timeout.</exception>
        protected IWebElement GetElement(By locator, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(this.driver, timeout);
            return wait.Until((d) => d.FindElement(locator));
        }
    }
}
