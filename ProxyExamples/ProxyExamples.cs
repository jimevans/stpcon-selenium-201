﻿// <copyright file="ProxyExamples.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace ProxyExamples
{
    using System;
    using System.Collections.Generic;
    using OpenQA.Selenium;
    using WebDriverExampleUtilities;

    /// <summary>
    /// Class containing sample code to demonstrate use of a proxy with WebDriver. 
    /// </summary>
    class ProxyExamples
    {
        private UrlBuilder builder;

        public ProxyExamples()
        {
            this.builder = new UrlBuilder();
        }

        /// <summary>
        /// Executes the code to test retrieval of HTTP status codes from the browser.
        /// </summary>
        /// <param name="driver">The driver to use with the browser.</param>
        public void TestStatusCodes(IWebDriver driver)
        {
            // Using Dave Haeffner's the-internet project http://github.com/arrgyle/the-internet,
            // which provides pages that return various HTTP status codes.
            string url = this.builder.BuildUrl("redirect");
            Console.WriteLine("Navigating to {0}", url);
            int responseCode = driver.NavigateTo(url);
            Console.WriteLine("Navigation to {0} returned response code {1}", url, responseCode);

            // Demonstrates navigating to a 404 page.
            url = this.builder.BuildUrl("redirector");
            Console.WriteLine("Navigating to {0}", url);
            responseCode = driver.NavigateTo(url);
            Console.WriteLine("Navigation to {0} returned response code {1}", url, responseCode);
            
            string elementId = "redirect";
            Console.WriteLine("Clicking on element with ID {0}", elementId);
            IWebElement element = driver.FindElement(By.Id(elementId));
            responseCode = element.ClickNavigate();
            Console.WriteLine("Element click returned response code {0}", responseCode);

            // Demonstrates navigating to a 404 page.
            url = this.builder.BuildUrl("status_codes/404");
            Console.WriteLine("Navigating to {0}", url);
            responseCode = driver.NavigateTo(url);
            Console.WriteLine("Navigation to {0} returned response code {1}", url, responseCode);
        }

        /// <summary>
        /// Executes the code to test detection of JavaScript errors from the browser.
        /// </summary>
        /// <param name="driver">The driver to use with the browser.</param>
        public void TestJavaScriptErrors(IWebDriver driver)
        {
            // Using Dave Haeffner's the-internet project http://github.com/arrgyle/the-internet,
            // which provides a page that contains a JavaScript error on page load.
            string url = this.builder.BuildUrl("javascript_error");
            Console.WriteLine("Navigating to {0}", url);
            driver.NavigateToWithErrorDetection(url);
            IList<string> javaScriptErrors = driver.GetJavaScriptErrors();
            if (javaScriptErrors == null)
            {
                Console.WriteLine("Could not access JavaScript errors collection. This is a catastrophic failure.");
            }
            else
            {
                if (javaScriptErrors.Count > 0)
                {
                    Console.WriteLine("Found the following JavaScript errors on the page:");
                    foreach (string javaScriptError in javaScriptErrors)
                    {
                        Console.WriteLine(javaScriptError);
                    }
                }
                else
                {
                    Console.WriteLine("No JavaScript errors found on the page.");
                }
            }
        }

        /// <summary>
        /// Executes the code to test navigation to page with basic authentication.
        /// </summary>
        /// <param name="driver">The driver to use with the browser.</param>
        public void TestAuthentication(IWebDriver driver)
        {
            // Using Dave Haeffner's the-internet project http://github.com/arrgyle/the-internet,
            // which provides a page that requires basic authentication.
            string url = this.builder.BuildUrl("basic_auth");
            Console.WriteLine("Navigating to {0}", url);
            int responseCode = driver.NavigateToWithAuthentication(url, "admin", "admin");
            Console.WriteLine("Navigation to {0} returned response code {1}", url, responseCode);
        }
    }
}
