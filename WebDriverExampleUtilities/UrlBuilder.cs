// <copyright file="UrlBuilder.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace WebDriverExampleUtilities
{
    using System;

    /// <summary>
    /// Builds URLs for use with example tests.
    /// </summary>
    public class UrlBuilder
    {
        public static readonly string DefaultBaseUrl = "http://the-internet.herokuapp.com";

        private Uri baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class using the
        /// default base URL.
        /// </summary>
        public UrlBuilder() : this(DefaultBaseUrl)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class using the
        /// specified base URL.
        /// </summary>
        /// <param name="baseUrl">Base URL to use in building relative URLs.</param>
        public UrlBuilder(string baseUrl)
        {
            this.baseUri = new Uri(baseUrl);
        }

        /// <summary>
        /// Builds a full URL from the base URL.
        /// </summary>
        /// <param name="relativePath">The relative path of the URL.</param>
        /// <returns>The full URL.</returns>
        public string BuildUrl(string relativePath)
        {
            Uri fullUrl = new Uri(this.baseUri, relativePath);
            return fullUrl.ToString();
        }
    }
}
