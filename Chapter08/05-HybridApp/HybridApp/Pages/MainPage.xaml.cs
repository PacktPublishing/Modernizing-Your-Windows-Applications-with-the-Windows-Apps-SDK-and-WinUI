using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace HybridApp.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MapService.ServiceToken = "IxVZeLFlv3YvlWGxBbqZ~px7HbChj1QuAnswVq4MBVw~AudKnqbk38mVm2KyKuALiK1pevSzOcqLRYW-vKpQvi-dcoMrjvbHtGzWDg7AkDYn";

            await MyWebView.EnsureCoreWebView2Async();
            MyWebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

        }

        private async void CoreWebView2_WebMessageReceived(CoreWebView2 sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            string city = args.TryGetWebMessageAsString();
            var result = await MapLocationFinder.FindLocationsAsync(city, null);
            txtCoordinates.Text = $"Latitude: {result.Locations[0].Point.Position.Latitude} - Longitude: {result.Locations[0].Point.Position.Longitude}";
        }

        private async void OnGetLocation(object sender, RoutedEventArgs e)
        {
            Geolocator locator = new Geolocator();
            var result = await locator.GetGeopositionAsync();
            string message = $"Latitude: {result.Coordinate.Point.Position.Latitude} - Longitude: {result.Coordinate.Point.Position.Longitude}";
            await MyWebView.EnsureCoreWebView2Async();
            MyWebView.CoreWebView2.PostWebMessageAsString(message);
        }
    }
}
