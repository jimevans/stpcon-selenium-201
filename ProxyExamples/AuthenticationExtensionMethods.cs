// <copyright file="AuthenticationExtensionMethods.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace ProxyExamples
{
    using System;
    using OpenQA.Selenium;
    using WebDriverExampleUtilities;

    /// <summary>
    /// A class of extension methods for a WebDriver instance to use
    /// a proxy for basic authentication.
    /// </summary>
    public static class AuthenticationExtensionMethods
    {
        /// <summary>
        /// The default timeout for retrieving the status code for authentication.
        /// </summary>
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Navigates to a specified URL, returning the HTTP status code of the navigation.
        /// </summary>
        /// <param name="driver">The driver used to navigate to the URL.</param>
        /// <param name="targetUrl">The URL to navigate to.</param>
        /// <param name="userName">The user name with which to login.</param>
        /// <param name="password">The password with which to login.</param>
        /// <returns>The HTTP status code of the navigation.</returns>
        public static int NavigateToWithAuthentication(this IWebDriver driver, string targetUrl, string userName, string password)
        {
            return NavigateToWithAuthentication(driver, targetUrl, userName, password, DefaultTimeout, false);
        }

        /// <summary>
        /// Navigates to a specified URL, returning the HTTP status code of the navigation.
        /// </summary>
        /// <param name="driver">The driver used to navigate to the URL.</param>
        /// <param name="targetUrl">The URL to navigate to.</param>
        /// <param name="userName">The user name with which to login.</param>
        /// <param name="password">The password with which to login.</param>
        /// <param name="timeout">A <see cref="TimeSpan"/> structure for the time out of the navigation.</param>
        /// <returns>The HTTP status code of the navigation.</returns>
        public static int NavigateToWithAuthentication(this IWebDriver driver, string targetUrl, string userName, string password, TimeSpan timeout)
        {
            return NavigateToWithAuthentication(driver, targetUrl, userName, password, timeout, false);
        }

        /// <summary>
        /// Navigates to a specified URL, returning the HTTP status code of the navigation.
        /// </summary>
        /// <param name="driver">The driver used to navigate to the URL.</param>
        /// <param name="targetUrl">The URL to navigate to.</param>
        /// <param name="userName">The user name with which to login.</param>
        /// <param name="password">The password with which to login.</param>
        /// <param name="timeout">A <see cref="TimeSpan"/> structure for the time out of the navigation.</param>
        /// <param name="printDebugInfo"><see langword="true"/> to print debugging information to the console;
        /// otherwise, <see langword="false"/>.</param>
        /// <returns>The HTTP status code of the navigation.</returns>
        public static int NavigateToWithAuthentication(this IWebDriver driver, string targetUrl, string userName, string password, TimeSpan timeout, bool printDebugInfo)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver", "Driver cannot be null");
            }

            if (string.IsNullOrEmpty(targetUrl))
            {
                throw new ArgumentException("URL cannot be null or the empty string.", "targetUrl");
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("User name cannot be null or the empty string.", "userName");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or the empty string.", "password");
            }

            int responseCode = 0;
            DateTime endTime = DateTime.Now.Add(timeout);
            ProxyManager.Instance.Proxy.OnRequestReceived = context =>
            {
                byte[] credentialsArray = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password));
                string encodedCredentials = Convert.ToBase64String(credentialsArray);
                context.RequestHeader.Authorization = string.Format("Basic {0}", encodedCredentials);
            };

            ProxyManager.Instance.Proxy.OnResponseSent = context =>
            {
                if (printDebugInfo)
                {
                    Console.WriteLine("DEBUG: Received response for resource with URL {0}", context.RequestHeader.RequestURI);
                }

                if (context.RequestHeader.RequestURI == targetUrl)
                {
                    responseCode = context.ResponseHeader.StatusCode;
                    if (printDebugInfo)
                    {
                        Console.WriteLine("DEBUG: Found response for {0}, setting response code.", context.RequestHeader.RequestURI);
                    }
                }
            };

            // Attach the event handler, perform the navigation, and wait for
            // the status code to be non-zero, or to timeout. Then detach the
            // event handler and return the response code.
            driver.Url = targetUrl;
            while (responseCode == 0 && DateTime.Now < endTime)
            {
                System.Threading.Thread.Sleep(100);
            }

            ProxyManager.Instance.Proxy.OnResponseSent = null;
            ProxyManager.Instance.Proxy.OnRequestReceived = null;
            return responseCode;
        }
    }
}
