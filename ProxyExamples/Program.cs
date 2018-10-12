using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverExampleUtilities;

namespace ProxyExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Note that we're using a port of 0, which tells the proxy to
            // select a random available port to listen on.
            int proxyPort = ProxyManager.Instance.StartProxyServer();

            // We are only proxying HTTP traffic, but could just as easily
            // proxy HTTPS or FTP traffic.
            OpenQA.Selenium.Proxy proxy = new OpenQA.Selenium.Proxy();
            proxy.HttpProxy = string.Format("127.0.0.1:{0}", proxyPort);

            // See the code of the individual methods for the details of how
            // to create the driver instance with the proxy settings properly set.
            BrowserKind browser = BrowserKind.Chrome;
            //BrowserKind browser = BrowserKind.Firefox;
            //BrowserKind browser = BrowserKind.IE;
            //BrowserKind browser = BrowserKind.Edge;
            //BrowserKind browser = BrowserKind.PhantomJS;

            IWebDriver driver = WebDriverFactory.CreateWebDriverWithProxy(browser, proxy);

            ProxyExamples proxyExamples = new ProxyExamples();
            proxyExamples.TestJavaScriptErrors(driver);
            //proxyExamples.TestStatusCodes(driver);

            driver.Quit();
            ProxyManager.Instance.StopProxyServer();

            Console.WriteLine("Complete! Press <Enter> to exit.");
            Console.ReadLine();
        }
    }
}
