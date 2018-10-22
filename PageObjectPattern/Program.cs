// <copyright file="Program.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace PageObjectPatternExamples
{
    using System;
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
            BrowserKind browserKind = BrowserKind.Chrome;
            string baseUrl = Constants.BaseUrl;

            PageObjectPatternExamples pageObjectPatternExamples = new PageObjectPatternExamples(browserKind, baseUrl);

            pageObjectPatternExamples.TestSuccessfulLogin();

            pageObjectPatternExamples.TestUnsuccessfulUserNameLogin();

            pageObjectPatternExamples.TestUnsuccessfulPasswordLogin();

            Console.WriteLine("Complete! Press <Enter> to exit.");
            Console.ReadLine();
        }
    }
}
