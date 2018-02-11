using System;

using Xamarin.Forms;
using System.Linq;

namespace Homeful.mobile
{
    public partial class RouteDetailPage : ContentPage
    {
        RouteDetailViewModel viewModel;

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
        void OnStopSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // TODO: go to camp
        }

        void Handle_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var stop = btn.BindingContext as Stop;
            var currentStop = viewModel.Route.Stops.Where(s => s.Camp.Id == stop.Camp.Id).SingleOrDefault();
            currentStop.Complete = !stop.Complete;
            btn.BackgroundColor = currentStop.Complete ? Color.Green : Color.LightGray;
        }
    }
}