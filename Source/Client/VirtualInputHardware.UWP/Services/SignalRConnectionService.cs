namespace VirtualInputHardware.UWP.Services
{
    using Contracts;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Transports;

    public class SignalRConnectionService : ISignalRConnectionService
    {
        public HubConnection Connection { get; private set; }

        public IHubProxy MouseHubProxy { get; private set; }

        public void InitConnection(string host, int port)
        {
            this.Connection = new HubConnection($"http://{host}:{port}");
            this.MouseHubProxy = Connection.CreateHubProxy("MouseHub");
            this.Connection.Start(new WebSocketTransport());
        }
    }
}
