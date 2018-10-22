// <copyright file="Program.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace ProxyExamples
{
    using System;
    using OpenQA.Selenium;
    using WebDriverExampleUtilities;

    /// <summary>
    /// Class containing the main application entry point and supporting methods.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        /// <param name="args">Command line arguments of the application.</param>
        static void Main(string[] args)
        {
            string baseUrl = Constants.BaseUrl;

            // Note that we're using a port of 0, which tells the proxy to
            // select a random available port to listen on.
            int proxyPort = ProxyManager.Instance.StartProxyServer();

            // We are only proxying HTTP traffic, but could just as easily
            // proxy HTTPS or FTP traffic.
            Proxy proxy = new Proxy();
            proxy.HttpProxy = string.Format("127.0.0.1:{0}", proxyPort);

            // See the code of the individual methods for the details of how
            // to create the driver instance with the proxy settings properly set.
            BrowserKind browser = BrowserKind.Chrome;
            //BrowserKind browser = BrowserKind.Firefox;
            //BrowserKind browser = BrowserKind.IE;
            //BrowserKind browser = BrowserKind.Edge;

            IWebDriver driver = WebDriverFactory.CreateWebDriverWithProxy(browser, proxy);

            ProxyExamples proxyExamples = new ProxyExamples();
            proxyExamples.TestStatusCodes(driver, baseUrl);
            //proxyExamples.TestJavaScriptErrors(driver, baseUrl);
            //proxyExamples.TestAuthentication(driver, baseUrl);

            driver.Quit();
            ProxyManager.Instance.StopProxyServer();

            Console.WriteLine("Complete! Press <Enter> to exit.");
            Console.ReadLine();
        }
    }
}
