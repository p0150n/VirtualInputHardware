namespace VirtualInputHardware.UWP.ViewModels
{
    using System;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Microsoft.AspNet.SignalR.Client;
    using Services.Contracts;

    public class ConnectionPageViewModel : ViewModelBase
    {
        private readonly ISignalRConnectionService signalRConnectionService;
        private readonly CoreDispatcher dispatcher;
        private string host = "192.168.100.7";
        private int port = 9000;
        private string connectionState;

        public ConnectionPageViewModel(ISignalRConnectionService signalRConnectionService, CoreDispatcher dispatcher)
        {
            this.signalRConnectionService = signalRConnectionService;
            this.dispatcher = dispatcher;
        }

        public string Host
        {
            get { return this.host; }
            set { this.SetProperty(ref this.host, value); }
        }
        
        public int Port
        {
            get { return this.port; }
            set { this.SetProperty(ref this.port, value); }
        }

        public string ConnectionState
        {
            get
            {
                if (this.connectionState == null)
                {
                    this.connectionState = this.signalRConnectionService.Connection?.State.ToString() ?? "Unknown";
                }

                return this.connectionState;
            }
            set { this.SetProperty(ref this.connectionState, value); }
        }

        public void Connect_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.signalRConnectionService.Connection != null)
            {
                this.signalRConnectionService.Connection.StateChanged -= ConnectionOnStateChanged;
                this.signalRConnectionService.Connection.Dispose();
            }

            this.signalRConnectionService.InitConnection(this.Host, this.Port);
            this.signalRConnectionService.Connection.StateChanged += ConnectionOnStateChanged;
        }

        private void ConnectionOnStateChanged(StateChange stateChange)
        {
            this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.ConnectionState = stateChange.NewState.ToString();
            });
        }
    }
}
