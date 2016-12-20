namespace VirtualInputHardware.UWP.Views
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Mvvm;
    using ViewModels;
    
    public sealed partial class ConnectionPage : Page
    {
        public ConnectionPage()
        {
            this.InitializeComponent();

            this.ViewModel = ViewModelFactory.ConnectionPage;
        }
        public ConnectionPageViewModel ViewModel
        {
            get { return this.DataContext as ConnectionPageViewModel; }
            set { this.DataContext = value; }
        }
    }
}
