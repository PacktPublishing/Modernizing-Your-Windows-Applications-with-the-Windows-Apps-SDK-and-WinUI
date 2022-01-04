using Microsoft.UI.Xaml;
using System;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace LocationServices
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            //get your key from https://www.bingmapsportal.com/
            MapService.ServiceToken = "<your key>";
        }

        private async void OnGetPosition(object sender, RoutedEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            if (geolocator.LocationStatus != PositionStatus.Disabled && geolocator.LocationStatus != PositionStatus.NotAvailable)
            {
                Geoposition position = await geolocator.GetGeopositionAsync();
                txtPosition.Text = $"{position.Coordinate.Point.Position.Latitude} - {position.Coordinate.Point.Position.Longitude}";

                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(position.Coordinate.Point);
                txtLocation.Text = result.Locations[0].DisplayName;
            }
        }
    }
}
