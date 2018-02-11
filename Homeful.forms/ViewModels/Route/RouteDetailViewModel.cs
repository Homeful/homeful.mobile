using System;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Homeful.mobile
{
    public class RouteDetailViewModel : RouteBaseViewModel
    {
        async void Stop_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await RouteDataStore.UpdateAsync(Route);
        }

        public Route Route { get; set; }

        public RouteDetailViewModel(Route route = null)
        {
            Title = route?.Name;
            Route = route;
        }

        public RouteDetailViewModel(string id)
        {
            LoadData(id);
        }

        private async void LoadData(string id)
        {
            Route = await RouteDataStore.GetAsync(id);

            this.Route.Stops.ForEach((stop) => stop.PropertyChanged += Stop_PropertyChanged);

            Title = Route?.Name;
        }
    }
}