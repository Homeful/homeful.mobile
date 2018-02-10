using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class RoutesPage : ContentPage
    {
        RoutesViewModel viewModel;

        public RoutesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RoutesViewModel();
        }

        async void OnRouteSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Route;
            if (item == null)
                return;

            await Navigation.PushAsync(new RouteDetailPage(new RouteDetailViewModel(item)));

            // Manually deselect item
            RoutesListView.SelectedItem = null;
        }

        async void AddRoute_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewRoutePage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Routes.Count == 0)
                viewModel.LoadRoutesCommand.Execute(null);
        }
    }
}
