using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Homeful.mobile
{
    public partial class CampDetailPage : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            string location = $"{viewModel.Camp.Object.Location.Lat}, {viewModel.Camp.Object.Location.Lng.ToString()}";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Device.OpenUri(
                        new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(location))));
                    break;
                case Device.Android:
                    Device.OpenUri(
                        new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(location))));
                    break;
                case Device.UWP:
                case Device.WinPhone:
                    Device.OpenUri(
                        new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(location))));
                    break;
            }
        }

        CampDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public CampDetailPage()
        {
            InitializeComponent();
            //var camp = new Camp
            //{
            //    Name = "Camp 1"
            //};

            //viewModel = new CampDetailViewModel(camp);
            //BindingContext = viewModel;
        }

        public CampDetailPage(CampDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            if (viewModel.Camp.Object.Location != null) 
            {
                var position = new Position(viewModel.Camp.Object.Location.Lat, viewModel.Camp.Object.Location.Lng);
                MyMap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(
                    position,
                    new Distance(1000)
                ));

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = viewModel.Camp.Object.Name
                };
                MyMap.Pins.Add(pin);
            }
        }
    }
}
