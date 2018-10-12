namespace ProxyExamples
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    class ProxyExamples
    {
        /// <summary>
        /// Executes the code to test retrieval of HTTP status codes from the browser.
        /// </summary>
        /// <param name="driver">The driver to use with the browser.</param>
        public void TestStatusCodes(IWebDriver driver)
        {
            // Using Dave Haeffner's the-internet project http://github.com/arrgyle/the-internet,
            // which provides pages that return various HTTP status codes.
            string url = "http://the-internet.herokuapp.com/redirect";
            Console.WriteLine("Navigating to {0}", url);
            int responseCode = driver.NavigateTo(url);
            Console.WriteLine("Navigation to {0} returned response code {1}", url, responseCode);

            // Demonstrates navigating to a 404 page.
            url = "http://the-internet.herokuapp.com/redirector";
            Console.WriteLine("Navigating to {0}", url);
            responseCode = driver.NavigateTo(url);
            Console.WriteLine("Navigation to {0} returned response code {1}", url, responseCode);
            
            string elementId = "redirect";
            Console.WriteLine("Clicking on element with ID {0}", elementId);
            IWebElement element = driver.FindElement(By.Id(elementId));
            responseCode = element.ClickNavigate();
            Console.WriteLine("Element click returned response code {0}", responseCode);

            // Demonstrates navigating to a 404 page.
            url = "http://the-internet.herokuapp.com/status_codes/404";
            Console.WriteLine("Navigating to {0}", url);
            responseCode = driver.NavigateTo(url);
            Console.WriteLine("Navigation to {0} returned response code {1}", url, responseCode);
        }

        public void TestJavaScriptErrors(IWebDriver driver)
        {
            // Using Dave Haeffner's the-internet project http://github.com/arrgyle/the-internet,
            // which provides pages that return various HTTP status codes.
            string url = "http://the-internet.herokuapp.com/javascript_error";
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
    }
}
