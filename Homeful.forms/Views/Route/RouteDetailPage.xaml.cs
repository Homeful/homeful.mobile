using System;

using Xamarin.Forms;
using System.Linq;
using Firebase.Database;
using System.Reflection;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

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
        void Route_Start_Clicked(object sender, EventArgs e)
        {
            var route = viewModel.Route;
            route.Active = !route.Active;
            RouteStatusButton.BorderColor = route.Active ? Color.Green : Color.Gray;
            RouteStatusButton.TextColor = route.Active ? Color.Green : Color.Gray;
            RouteStatusButton.Text = route.Active ? "In Progress" : "Start";
            SetCurrentRoute(route);
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
        private void SetCurrentRoute(Route route)
        {
            viewModel.Route.Active = route.Active;

            List<Route> currentRoutes = new List<Route>();

            if (Application.Current.Properties.ContainsKey("routes"))
            {
                currentRoutes = Application.Current.Properties["routes"] as List<Route>;
            }

            if (route.Active)
            {
                AddRouteToList(currentRoutes, route);
                StartRoute(route);
            }
            else
            {
                RemoveRouteFromList(currentRoutes, route);
                StopRoute(route);
            }
            viewModel.UpdateRoute();
        }
        private void StartRoute(Route route)
        {
            if (viewModel.Stops.ToList().All(s => !s.Object.InProgress))
            {
                var firstStop = viewModel.Stops.ToList().First();
                firstStop.Object.InProgress = true;
                viewModel.UpdateStop(firstStop);
            }
        }
        private void StopRoute(Route route)
        {
            viewModel.Stops.ToList().ForEach(s =>
            {
                if (s.Object.InProgress)
                {
                    s.Object.InProgress = false;
                    viewModel.UpdateStop(s);
                }
            });
        }
        private void SetCurrentStop(Route route)
        {
            var stopKey = GetKeyOfCurrentStop(route);
            if (stopKey != null)
            {
                // get the current stop in progress
                currentStop = viewModel.Stops.ToList().FirstOrDefault(s => s.Key == stopKey);
                var index = viewModel.Stops.ToList().IndexOf(currentStop);

                // set it to complete
                currentStop.Object.InProgress = false;
                currentStop.Object.Complete = true;
                currentStop.Object.CompletedAt = DateTime.Now;

                // set the next stop to in progress
                currentStop = viewModel.Stops.ToList().ElementAt(index++);
                currentStop.Object.InProgress = true;
            }
            else
            {
                currentStop = viewModel.Stops.ToList().First();
                currentStop.Object.InProgress = true;
            }
        }
        private bool RouteIsInList(List<Route> currentRoutes, Route route)
        {
            return currentRoutes != null && currentRoutes.Exists(r => r.Id == route.Id);
        }
        private void AddRouteToList(List<Route> currentRoutes, Route route)
        {
            if (!RouteIsInList(currentRoutes, route))
            {
                currentRoutes.Add(route);
                if (Application.Current.Properties.ContainsKey("routes"))
                {
                    Application.Current.Properties["routes"] = currentRoutes;
                }
                else
                {
                    Application.Current.Properties.Add("routes", currentRoutes);
                }
            }
        }
        private void RemoveRouteFromList(List<Route> currentRoutes, Route routeToRemove)
        {
            var index = GetIndexOfRoute(currentRoutes, routeToRemove);
            if (index >= 0) currentRoutes.RemoveAt(index);

            if (Application.Current.Properties.ContainsKey("routes"))
            {
                Application.Current.Properties["routes"] = currentRoutes;
            }
            else
            {
                Application.Current.Properties.Add("routes", currentRoutes);
            }
        }
        private int GetIndexOfRoute(List<Route> currentRoutes, Route route)
        {
            for (int i = 0; i < currentRoutes.Count(); i++)
            {
                if (currentRoutes[i].Id == route.Id) return i;
            }
            return -1;
        }
        private string GetKeyOfCurrentStop(Route route)
        {
            var stops = route.Stops;
            foreach (var stop in stops)
            {
                if (stop.Value.InProgress) return stop.Key;
            }
            return null;
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

        public void OnRemoveStop(object sender, EventArgs e)
        {
            var stop = ((MenuItem)sender).CommandParameter as FirebaseObject<Stop>;
            viewModel.RemoveStop(stop);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Stops.Count() == 0)
                viewModel.LoadStopsCommand.Execute(null);
        }
    }
}