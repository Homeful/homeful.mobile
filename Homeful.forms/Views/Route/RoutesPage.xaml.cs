﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Firebase.Database;

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
            var item = args.SelectedItem as FirebaseObject<Route>;
            if (item == null)
                return;

            await Navigation.PushAsync(new RouteDetailPage(new RouteDetailViewModel(item.Key)));

            // Manually deselect item
            RoutesListView.SelectedItem = null;
        }

        async void AddRoute_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewRoutePage());
        }

        async void OnRemove(object sender, EventArgs e)
        {
            var route = ((MenuItem)sender).CommandParameter as FirebaseObject<Route>;
            await viewModel.Remove(route);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Routes.Count == 0)
                viewModel.LoadRoutesCommand.Execute(null);
        }
    }
}