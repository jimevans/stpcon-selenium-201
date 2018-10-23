// <copyright file="Program.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace WaitExamples
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
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
            UrlBuilder builder = new UrlBuilder();
            IWebDriver driver = WebDriverFactory.CreateWebDriver(BrowserKind.Chrome);
            driver.Url = builder.BuildUrl("dynamic_loading/1");

            driver.FindElement(By.CssSelector("button")).Click();

            IWebElement finishElement = OnlyFindElement(driver);
            //IWebElement finishElement = ImplicitWaitFindElement(driver);
            //IWebElement finishElement = ExplicitWaitFindElement(driver);
            //IWebElement finishElement = CustomWaitFindElement(driver);

            Console.WriteLine("Finish element text is: {0}", finishElement.Text);
            driver.Quit();

            Console.WriteLine("Complete! Press <Enter> to exit.");
            Console.ReadLine();
        }

        static IWebElement OnlyFindElement(IWebDriver driver)
        {
            return driver.FindElement(By.Id("finish"));
        }

        static IWebElement ImplicitWaitFindElement(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return driver.FindElement(By.Id("finish"));
        }

        static IWebElement ExplicitWaitFindElement(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            return wait.Until((d) => d.FindElement(By.Id("finish")));
        }

        static IWebElement CustomWaitFindElement(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            return wait.Until<IWebElement>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.Id("finish"));
                    if (element.Displayed)
                    {
                        return element;
                    }
                }
                catch (NoSuchElementException)
                {
                }

                return null;
            });
        }
    }
}
