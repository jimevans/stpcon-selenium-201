// <copyright file="MiscellaneousSamples.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace LightningRoundExamples
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;

    public class MiscellaneousSamples
    {
        public void FileUpload(IWebDriver driver)
        {
            IWebElement fileElement = driver.FindElement(By.Id("file-upload"));

            // DO NOT DO THIS! It will throw an exception.
            // Use fileElement.SendKeys("path\to\file\to\upload") instead.
            fileElement.Click();
        }

        public void FileDownload(IWebDriver driver)
        {
            IWebElement fileElement = driver.FindElement(By.XPath("//a[contains(text(), 'upload_test.png')]"));
            fileElement.Click();
        }

        public void LocatorsOfAllKinds(IWebDriver driver)
        {
            // This locator will never work!
            By illegal = By.CssSelector("a:contains('hello')");

            // This is the proper way to find by element text.
            By textLocator = By.XPath("//a[contains(text(), 'hello')]");

            // Can even use custom locators!
            By findByAttribute = Via.AttributeValue("data-value", "myvalue");
            By findByJavaScript = Via.JavaScript("return document.getElementById('foo');");
        }

        public void SinglePageAppWait(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.Id("myElement"));
            string oldValue = element.Text;

            IWebElement clickElement = driver.FindElement(By.Id("refreshElement"));
            clickElement.Click();

            // We are waiting for the element to go stale before proceeding.
            // This is an indication that the page refresh has started.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until((d) =>
            {
                try
                {
                    string tag = element.TagName;
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            });

            element = driver.FindElement(By.Id("myElement"));
            string newValue = element.Text;
        }
    }
}
