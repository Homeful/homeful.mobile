using System;

using Xamarin.Forms;
using System.Linq;
using Firebase.Database;
using System.Reflection;

namespace Homeful.mobile
{
    public partial class RouteDetailPage : ContentPage
    {
        RouteDetailViewModel viewModel;
        private static int clickCount;
        private FirebaseObject<Stop> currentStop;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public RouteDetailPage()
        {
            InitializeComponent();

            var route = new Route
            {
                Name = "Route 1"
            };

            viewModel = new RouteDetailViewModel(route);
            BindingContext = viewModel;
        }

        public RouteDetailPage(RouteDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        void Start_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var stop = btn.BindingContext as FirebaseObject<Stop>;
            SetCurrentStop(stop);
            currentStop.Object.InProgress = true;
            currentStop.Object.Complete = false;
            viewModel.UpdateStop(currentStop);
        }
        void InProgress_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var stop = btn.BindingContext as FirebaseObject<Stop>;
            SetCurrentStop(stop);
            currentStop.Object.InProgress = false;
            currentStop.Object.Complete = true;
            viewModel.UpdateStop(currentStop);
        }
        void Complete_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var stop = btn.BindingContext as FirebaseObject<Stop>;
            SetCurrentStop(stop);
            if (clickCount < 1)
            {
                TimeSpan tt = new TimeSpan(0, 0, 1);
                Device.StartTimer(tt, ClickHandle);
            }
            clickCount++;
        }
        private void SetCurrentStop(FirebaseObject<Stop> stop)
        {
            currentStop = viewModel.Stops.ToList().Where(s => s.Key == stop.Key).FirstOrDefault();
        }
        private bool ClickHandle()
        {
            if (clickCount > 1)
            {
                currentStop.Object.Complete = false;
                currentStop.Object.InProgress = false;
                viewModel.UpdateStop(currentStop);
            }
            clickCount = 0;
            return false;
        }
        async void OnStopSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var stop = args.SelectedItem as Stop;
            if (stop == null)
                return;

            await Navigation.PushAsync(new CampDetailPage(new CampDetailViewModel(viewModel.Route.Id, stop.Camp.Id)));

            RouteStopsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Stops.Count() == 0)
                viewModel.LoadStopsCommand.Execute(null);
        }
    }
}