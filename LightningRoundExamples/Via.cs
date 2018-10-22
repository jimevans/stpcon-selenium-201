// <copyright file="Via.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace LightningRoundExamples
{
    using System;
    using System.Collections.ObjectModel;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Internal;

    public class Via : By
    {
        private Via()
        {
        }

        public static By AttributeValue(string attributeName, string attributeValue)
        {
            Via byAttribute = new Via();
            byAttribute.FindElementMethod = (ISearchContext context) => { return FindElementByAttributeValue(context, attributeName, attributeValue); };
            byAttribute.FindElementsMethod = (ISearchContext context) => { return FindElementsByAttributeValue(context, attributeName, attributeValue); };
            return byAttribute;
        }

        public static By JavaScript(string script)
        {
            Via byJavaScript = new Via();
            byJavaScript.FindElementMethod = (ISearchContext context) => { return FindElementByJavaScript(context, script); };
            byJavaScript.FindElementsMethod = (ISearchContext context) => { return FindElementsByJavaScript(context, script); };
            return byJavaScript;
        }

        private static ReadOnlyCollection<IWebElement> FindElementsByAttributeValue(ISearchContext context, string attributeName, string attributeValue)
        {
            string attributeSelector = string.Format("[{0} = '{1}']", attributeName, attributeValue);
            return context.FindElements(By.CssSelector(attributeSelector));
        }

        private static IWebElement FindElementByAttributeValue(ISearchContext context, string attributeName, string attributeValue)
        {
            string attributeSelector = string.Format("[{0} = '{1}']", attributeName, attributeValue);
            return context.FindElement(By.CssSelector(attributeSelector));
        }

        private static IWebElement FindElementByJavaScript(ISearchContext arg, string script)
        {
            object result = ExecuteJavaScript(arg, script);
            IWebElement castResult = result as IWebElement;
            if (castResult == null)
            {
                throw new NotFoundException("Script does not return an element");
            }

            return castResult;
        }

        private static ReadOnlyCollection<IWebElement> FindElementsByJavaScript(ISearchContext arg, string script)
        {
            object result = ExecuteJavaScript(arg, script);
            ReadOnlyCollection<IWebElement> castResult = result as ReadOnlyCollection<IWebElement>;
            if (castResult == null)
            {
                throw new NotFoundException("Script does not return an element");
            }

            return castResult;
        }

        private static object ExecuteJavaScript(ISearchContext arg, string script)
        {
            IJavaScriptExecutor executor = arg as IJavaScriptExecutor;
            if (executor == null)
            {
                IWrapsDriver driverWrapper = arg as IWrapsDriver;
                if (driverWrapper != null)
                {
                    executor = driverWrapper.WrappedDriver as IJavaScriptExecutor;
                }
            }

            if (executor == null)
            {
                throw new InvalidOperationException("Search context cannot execute JavaScript");
            }

            object scriptResult = executor.ExecuteScript(script);
            return scriptResult;
        }
    }
}
