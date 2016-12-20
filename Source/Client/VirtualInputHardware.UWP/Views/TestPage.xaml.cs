// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VirtualInputHardware.UWP.Views
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestPage : Page
    {
        public TestPage()
        {
            this.InitializeComponent();
        }

        private void GoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var url = this.UrlTextBox.Text;
            this.MyWebView.Navigate(new Uri(url));
        }

        private void StartWebServer_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
