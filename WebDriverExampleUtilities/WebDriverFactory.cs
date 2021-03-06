﻿// <copyright file="WebDriverFactory.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace WebDriverExampleUtilities
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Edge;

    /// <summary>
    /// A static factory class for creating WebDriver instances with proxies.
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        /// Creates a WebDriver instance for the desired browser.
        /// </summary>
        /// <param name="kind">The browser to launch.</param>
        /// <returns>A WebDriver instance for the specified browser.</returns>
        public static IWebDriver CreateWebDriver(BrowserKind kind)
        {
            IWebDriver driver = null;
            switch (kind)
            {
                case BrowserKind.InternetExplorer:
                    driver = new InternetExplorerDriver();
                    break;

                case BrowserKind.Firefox:
                    driver = new FirefoxDriver();
                    break;

                case BrowserKind.Chrome:
                    driver = new ChromeDriver();
                    break;

                case BrowserKind.Edge:
                    driver = new EdgeDriver();
                    break;
            }

            return driver;
        }

        /// <summary>
        /// Creates a WebDriver instance for the desired browser using the specified proxy settings.
        /// </summary>
        /// <param name="kind">The browser to launch.</param>
        /// <param name="proxy">The WebDriver Proxy object containing the proxy settings.</param>
        /// <returns>A WebDriver instance using the specified proxy settings.</returns>
        public static IWebDriver CreateWebDriverWithProxy(BrowserKind kind, Proxy proxy)
        {
            IWebDriver driver = null;
            switch (kind)
            {
                case BrowserKind.InternetExplorer:
                    driver = CreateInternetExplorerDriverWithProxy(proxy);
                    break;

                case BrowserKind.Firefox:
                    driver = CreateFirefoxDriverWithProxy(proxy);
                    break;

                case BrowserKind.Chrome:
                    driver = CreateChromeDriverWithProxy(proxy);
                    break;

                case BrowserKind.Edge:
                    driver = CreateEdgeDriverWithProxy(proxy);
                    break;
            }

            return driver;
        }

        /// <summary>
        /// Creates an InternetExplorerDriver instance using the specified proxy settings.
        /// </summary>
        /// <param name="proxy">The WebDriver Proxy object containing the proxy settings.</param>
        /// <returns>An InternetExplorerDriver instance using the specified proxy settings</returns>
        private static IWebDriver CreateInternetExplorerDriverWithProxy(Proxy proxy)
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.Proxy = proxy;

            // Make IE not use the system proxy, and clear its cache before
            // launch. This makes the behavior of IE consistent with other
            // browsers' behavior.
            options.UsePerProcessProxy = true;
            options.EnsureCleanSession = true;

            IWebDriver driver = new InternetExplorerDriver(options);
            return driver;
        }

        /// <summary>
        /// Creates an FirefoxDriver instance using the specified proxy settings.
        /// </summary>
        /// <param name="proxy">The WebDriver Proxy object containing the proxy settings.</param>
        /// <returns>A FirefoxDriver instance using the specified proxy settings</returns>
        private static IWebDriver CreateFirefoxDriverWithProxy(Proxy proxy)
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.Proxy = proxy;

            IWebDriver driver = new FirefoxDriver(firefoxOptions);
            return driver;
        }

        /// <summary>
        /// Creates an ChromeDriver instance using the specified proxy settings.
        /// </summary>
        /// <param name="proxy">The WebDriver Proxy object containing the proxy settings.</param>
        /// <returns>A ChromeDriver instance using the specified proxy settings</returns>
        private static IWebDriver CreateChromeDriverWithProxy(Proxy proxy)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.Proxy = proxy;

            IWebDriver driver = new ChromeDriver(chromeOptions);
            return driver;
        }

        /// <summary>
        /// Creates an EdgeDriver instance using the specified proxy settings.
        /// </summary>
        /// <param name="proxy">The WebDriver Proxy object containing the proxy settings.</param>
        /// <returns>An EdgeDriver instance using the specified proxy settings</returns>
        private static IWebDriver CreateEdgeDriverWithProxy(Proxy proxy)
        {
            EdgeOptions edgeOptions = new EdgeOptions();

            IWebDriver driver = new EdgeDriver(edgeOptions);
            return driver;
        }
    }
}
