// <copyright file="ProxyManager.cs" company="Jim Evans">
// Copyright © 2018 Jim Evans
// Licensed under the Apache 2.0 license, as found in the LICENSE file accompanying this source code.
// </copyright>

namespace WebDriverExampleUtilities
{
    using System;
    using System.Net;
    using FryProxy;

    /// <summary>
    /// Singleton class that creates an instance of a web proxy, in this case,
    /// using FryProxy (https://github.com/eger-geger/FryProxy).
    /// </summary>
    public class ProxyManager
    {
        private static readonly Lazy<ProxyManager> instance = new Lazy<ProxyManager>(() => new ProxyManager());

        private HttpProxyServer server;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProxyManager"/> class.
        /// </summary>
        private ProxyManager()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ProxyManager"/> class.
        /// </summary>
        public static ProxyManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="HttpProxy"/> object created by the <see cref="HttpProxyServer"/>.
        /// </summary>
        public HttpProxy Proxy
        {
            get { return this.server.Proxy; }
        }

        /// <summary>
        /// Starts the <see cref="HttpProxyServer"/> listening for connection using a random port.
        /// </summary>
        /// <returns>The port on which the proxy server is listening.</returns>
        public int StartProxyServer()
        {
            // Specifying a port of zero tells the proxy server to pick a random
            // port to which to bind.
            return this.StartProxyServer(0);
        }

        /// <summary>
        /// Starts the <see cref="HttpProxyServer"/> listening for connection using a specified port.
        /// </summary>
        /// <returns>The port on which the proxy server is listening.</returns>
        public int StartProxyServer(int port)
        {
            IPEndPoint proxyEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            this.server = new HttpProxyServer("localhost", new HttpProxy());
            this.server.Start().WaitOne();
            return this.server.ProxyEndPoint.Port;
        }

        /// <summary>
        /// Stops the running <see cref="HttpProxyServer"/> from listening for connections.
        /// </summary>
        public void StopProxyServer()
        {
            this.server.Stop();
        }
    }
}
