namespace VirtualInputHardware.UWP.Views
{
    using Windows.Graphics.Display;
    using Windows.System.Display;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;
    using Autofac;
    using Mvvm;
    using Services.Contracts;
    using ViewModels;
    
    public sealed partial class MousePage : Page
    {

        private DisplayRequest keepScreenOnRequest;

        public MousePage()
        {
            this.InitializeComponent();

            this.ViewModel = ViewModelFactory.MousePage;
        }

        public MousePageViewModel ViewModel
        {
            get { return this.DataContext as MousePageViewModel; }
            set { this.DataContext = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (keepScreenOnRequest == null)
            {
                this.keepScreenOnRequest = new DisplayRequest();
            }

            this.keepScreenOnRequest.RequestActive();
            //DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.keepScreenOnRequest.RequestRelease();
            //DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
        }
    }
}
