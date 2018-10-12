using FryProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExampleUtilities
{
    public class ProxyManager
    {
        private static readonly Lazy<ProxyManager> instance = new Lazy<ProxyManager>(() => new ProxyManager());

        private HttpProxyServer server;

        private ProxyManager()
        {
        }

        public static ProxyManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public HttpProxy Proxy
        {
            get { return this.server.Proxy; }
        }

        public int StartProxyServer()
        {
            return this.StartProxyServer(0);
        }

        public int StartProxyServer(int port)
        {
            IPEndPoint proxyEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            this.server = new HttpProxyServer("localhost", new HttpProxy());
            this.server.Start().WaitOne();
            return this.server.ProxyEndPoint.Port;
        }

        public void StopProxyServer()
        {
            this.server.Stop();
        }
    }
}
