using System;
using System.Windows;
using Windows.Devices.Geolocation;

namespace WpfWebView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }

        private async void CoreWebView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            string message = e.TryGetWebMessageAsString();
            Geolocator geolocator = new Geolocator();
            var result = await geolocator.GetGeopositionAsync();
            string coordinates = $"{message} - Latitude: {result.Coordinate.Point.Position.Latitude} - Longitude: {result.Coordinate.Point.Position.Longitude}";
            progressBar.Visibility = Visibility.Collapsed;
            MessageBox.Show(coordinates);
        }

        private void OnGoClicked(object sender, RoutedEventArgs e)
        {
            if (Uri.IsWellFormedUriString(txtUrl.Text, UriKind.Absolute))
            {
                webView.CoreWebView2.Navigate(txtUrl.Text);
            }
            else
            {
                MessageBox.Show("URL not valid");
            }
        }

        private void OnHomeClicked(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.Navigate("https://contosoexpensescd.z19.web.core.windows.net/WebViewApp");
        }
    }
}
