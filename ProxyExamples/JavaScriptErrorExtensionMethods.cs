// <copyright file="JavaScriptErrorExtensionMethods.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace ProxyExamples
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using FryProxy.Writers;
    using OpenQA.Selenium;
    using WebDriverExampleUtilities;

    /// <summary>
    /// A class of extension methods for a WebDriver instance to use a proxy
    /// to detect and retrieve JavaScript errors.
    /// </summary>
    public static class JavaScriptErrorExtensionMethods
    {
        /// <summary>
        /// The default timeout for retrieving JavaScript errors.
        /// </summary>
        private static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Navigates to a specified URL, injecting code to capture JavaScript errors on the page.
        /// </summary>
        /// <param name="driver">The driver used to navigate to the URL.</param>
        /// <param name="targetUrl">The URL to navigate to.</param>
        /// <exception cref="ArgumentNullException">Thrown if the driver instance or URL is null.</exception>
        public static void NavigateToWithErrorDetection(this IWebDriver driver, string targetUrl)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver", "Driver cannot be null");
            }

            if (string.IsNullOrEmpty(targetUrl))
            {
                throw new ArgumentNullException("targetUrl", "URL cannot be null or empty string");
            }

            ProxyManager.Instance.Proxy.OnResponseReceived = context =>
            {
                if (context.RequestHeader.RequestURI == targetUrl &&
                    context.ResponseHeader.EntityHeaders.ContentType.Contains("html"))
                {
                    byte[] buffer;
                    using (var stream = new MemoryStream())
                    {
                        new HttpMessageWriter(stream).Write(context.ResponseHeader, context.ServerStream);
                        buffer = stream.ToArray();
                    }
                    string headerString = context.ResponseHeader.ToString();
                    string responseBody = Encoding.UTF8.GetString(buffer);
                    responseBody = responseBody.Remove(0, headerString.Length);

                    string errorScript = "window.__webdriver_javascript_errors = []; window.onerror = function(errorMsg, url, line) { window.__webdriver_javascript_errors.push(errorMsg + ' (found at ' + url + ', line ' + line + ')'); };";
                    Regex regex = new Regex("<head.*>", RegexOptions.IgnoreCase);
                    Match match = regex.Match(responseBody);
                    string replacement = match.Value + "<script>" + errorScript + "</script>";
                    string newBody = regex.Replace(responseBody, replacement, 1);
                    byte[] responseBuffer = Encoding.UTF8.GetBytes(newBody);
                    using (var stream = new MemoryStream(responseBuffer))
                    {
                        // Must reset the Content-Length header to the appropriate new value.
                        context.ResponseHeader.EntityHeaders.ContentLength = stream.Length;
                        new HttpMessageWriter(context.ClientStream).Write(context.ResponseHeader, stream, stream.Length);
                        context.ClientStream.Flush();
                    }
                }
            };

            driver.Url = targetUrl;
            ProxyManager.Instance.Proxy.OnResponseReceived = null;
        }

        /// <summary>
        /// Gets the JavaScript errors on the current page.
        /// </summary>
        /// <param name="driver">The driver used to retrieve the errors.</param>
        /// <returns>A list of all JavaScript errors captured on the page.</returns>
        public static IList<string> GetJavaScriptErrors(this IWebDriver driver)
        {
            return GetJavaScriptErrors(driver, DefaultTimeout);
        }

        /// <summary>
        /// Gets the JavaScript errors on the current page.
        /// </summary>
        /// <param name="driver">The driver used to retrieve the errors.</param>
        /// <param name="timeout">A <see cref="TimeSpan"/> structure for the time out of the retrieval.</param>
        /// <returns>A list of all JavaScript errors captured on the page.</returns>
        public static IList<string> GetJavaScriptErrors(this IWebDriver driver, TimeSpan timeout)
        {
            string errorRetrievalScript = "var errorList = window.__webdriver_javascript_errors; window.__webdriver_javascript_errors = []; return errorList;";
            DateTime endTime = DateTime.Now.Add(timeout);
            List<string> errorList = new List<string>();
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            ReadOnlyCollection<object> returnedList = executor.ExecuteScript(errorRetrievalScript) as ReadOnlyCollection<object>;
            while (returnedList == null && DateTime.Now < endTime)
            {
                System.Threading.Thread.Sleep(250);
                returnedList = executor.ExecuteScript(errorRetrievalScript) as ReadOnlyCollection<object>;
            }

            if (returnedList == null)
            {
                return null;
            }
            else
            {
                foreach (object returnedError in returnedList)
                {
                    errorList.Add(returnedError.ToString());
                }
            }

            return errorList;
        }
    }
}
