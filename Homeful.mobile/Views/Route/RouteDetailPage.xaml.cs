using System;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class RouteDetailPage : ContentPage
    {
        RouteDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public RouteDetailPage()
        {
            InitializeComponent();

            var camp = new Route
            {
                Name = "Route 1"
            };

            viewModel = new RouteDetailViewModel(camp);
            BindingContext = viewModel;
        }

        public RouteDetailPage(RouteDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
