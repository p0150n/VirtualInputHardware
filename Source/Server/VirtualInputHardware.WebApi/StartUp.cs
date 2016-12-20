namespace VirtualInputHardware.WebApi
{
    using System;
    using System.Runtime.InteropServices;
    using Topshelf;

    public class StartUp
    {
        //[DllImport("kernel32.dll")]
        //static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //const int SW_HIDE = 0;
        //const int SW_SHOW = 5;

        public static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.SetServiceName("Remote control p0150n");
                x.SetDisplayName("Remote control p0150n");
                x.RunAsNetworkService();
                x.StartAutomatically();
                x.Service<OwinService>(s =>
                {
                    s.ConstructUsing(() => new OwinService());
                    s.WhenStarted(service =>
                    {
                        service.Start();
                        //var handle = GetConsoleWindow();
                        //ShowWindow(handle, SW_HIDE);
                    });
                    s.WhenStopped(service => service.Stop());
                });
            });
        }
    }
}
