﻿using System;

using Xamarin.Forms;
using System.Linq;

namespace Homeful.mobile
{
    public partial class RouteDetailPage : ContentPage
    {
        RouteDetailViewModel viewModel;
        private static int clickCount;
        private Stop currentStop;

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
            var stop = btn.BindingContext as Stop;
            SetCurrentStop(stop);

            currentStop.InProgress = true;
            currentStop.Complete = false;
        }
        void InProgress_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var stop = btn.BindingContext as Stop;
            SetCurrentStop(stop);
            currentStop.InProgress = false;
            currentStop.Complete = true;
        }
        void Complete_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var stop = btn.BindingContext as Stop;
            SetCurrentStop(stop);
            if (clickCount < 1)
            {
                TimeSpan tt = new TimeSpan(0, 0, 1);
                Device.StartTimer(tt, ClickHandle);
            }
            clickCount++;
            //var stop = btn.BindingContext as Stop;
            //var currentStop = viewModel.Route.Stops.Where(s => s.Camp.Id == stop.Camp.Id).SingleOrDefault();

            //currentStop.InProgress = false;
            //currentStop.Complete = false;
        }
        private void SetCurrentStop(Stop stop)
        {
            currentStop = viewModel.Route.Stops.Where(s => s.Camp.Id == stop.Camp.Id).SingleOrDefault();
        }
        private bool ClickHandle()
        {
            if (clickCount > 1)
            {
                currentStop.Complete = false;
                currentStop.InProgress = false;
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
    }
}