namespace VirtualInputHardware.UWP.Views.Base
{
    using Windows.UI.Xaml.Controls;

    public sealed partial class DrillInPage : Page
    {
        public DrillInPage()
        {
            this.InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(
                typeof(VirtualInputHardware.UWP.Views.Base.BasicSubPage),
                e.ClickedItem,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
