using System;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Homeful.mobile
{
    public partial class NewCampPage : ContentPage
    {
        public Camp Camp { get; set; }
        MapSpan mapSpan;

        public NewCampPage()
        {
            InitializeComponent();

            Camp = new Camp();

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Camp.Location = new Location()
            {
                Lat = mapSpan.Center.Latitude,
                Lng = mapSpan.Center.Longitude
            };
            MessagingCenter.Send(this, "AddCamp", Camp);
            await Navigation.PopToRootAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            var currentLocation = new Position(position.Latitude, position.Longitude);
            mapSpan = MapSpan.FromCenterAndRadius(currentLocation, Distance.FromMeters(500));
            MyMap.MoveToRegion(mapSpan);
        }
    }
}
