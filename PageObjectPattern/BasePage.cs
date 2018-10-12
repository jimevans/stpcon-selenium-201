using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectPattern
{
    public class BasePage
    {
        private IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        protected IWebDriver Driver
        {
            get { return this.driver; }
        }

        protected IWebElement GetElement(By locator)
        {
            return this.driver.FindElement(locator);
        }

        protected IWebElement GetElement(By locator, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(this.driver, timeout);
            return wait.Until((d) => d.FindElement(locator));
        }
    }
}
