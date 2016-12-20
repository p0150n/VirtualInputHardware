namespace VirtualInputHardware.UWP.Services.Contracts
{
    using Microsoft.AspNet.SignalR.Client;

    public interface ISignalRConnectionService
    {
        HubConnection Connection { get; }

        IHubProxy MouseHubProxy { get; }

        void InitConnection(string host, int port);
    }
}