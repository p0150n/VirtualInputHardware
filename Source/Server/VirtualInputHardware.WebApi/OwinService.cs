namespace VirtualInputHardware.WebApi
{
    using System;
    using Configurations;
    using Microsoft.Owin.Hosting;

    public class OwinService
    {
        private IDisposable webApp;

        public void Start()
        {   // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            //string url = "http://localhost:9000";
            string url = "http://*:9000";

            var options = new StartOptions(url)
            {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };

            this.webApp = WebApp.Start<StartOwin>(options);
        }

        public void Stop()
        {
            this.webApp.Dispose();
        }
    }
}