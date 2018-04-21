using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;
using SegmentedControl.FormsPlugin.Abstractions;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;

namespace Homeful.mobile
{
    public partial class CampsPage : ContentPage
    {
        CampsViewModel viewModel;

        public CampsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CampsViewModel();
        }

        async void OnCampSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var camp = args.SelectedItem as FirebaseObject<Camp>;
            if (camp == null)
                return;

            await Navigation.PushAsync(new CampDetailPage(new CampDetailViewModel(camp)));

            // Manually deselect camp
            CampsListView.SelectedItem = null;
        }

        async void AddCamp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCampPage());
        }

        async void OnRemove(object sender, EventArgs e)
        {
            var camp = ((MenuItem)sender).CommandParameter as FirebaseObject<Camp>;
            await viewModel.Remove(camp);
        }

        public void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    //SegContent.Children.Clear();
                    CampsListView.IsVisible = true;
                    CampsMapView.IsVisible = false;
                    break;
                case 1:
                    //SegContent.Children.Clear();
                    CampsListView.IsVisible = false;
                    CampsMapView.IsVisible = true;
                    AddCampsToMap();
                    break;
            }
        }

        private void SetMapDefaultPosition()
        {
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(36.1627, -86.7816), Distance.FromMiles(10)));
        }

        private void AddCampsToMap()
        {
            viewModel.Camps.ForEach(c => AddCampToMap(c.Object));
        }

        private void AddCampToMap(Camp camp)
        {
            var position = new Position(camp.Location.Lat, camp.Location.Lng); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.SavedPin,
                Position = position,
                Label = camp.Name
            };
            MyMap.Pins.Add(pin);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Camps.Count() == 0)
            {
                viewModel.LoadCampsCommand.Execute(null);
            }
            SetMapDefaultPosition();
        }
    }
}
