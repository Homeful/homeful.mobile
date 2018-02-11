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
    }
}