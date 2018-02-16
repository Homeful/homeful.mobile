using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Homeful.mobile
{
    public class RouteDetailViewModel : RouteBaseViewModel
    {
        public Command LoadStopsCommand { get; set; }

        async void Stop_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await RouteDataStore.UpdateAsync(Route);
        }

        public Route Route { get; set; }

        public RouteDetailViewModel(Route route = null)
        {
            Title = route?.Name;
            Route = route;
            LoadStopsCommand = new Command(async () => await ExecuteLoadStopsCommand());
        }

        public RouteDetailViewModel(string id)
        {
            LoadData(id);
            LoadStopsCommand = new Command(async () => await ExecuteLoadStopsCommand());
        }

        private async void LoadData(string id)
        {
            Route = await RouteDataStore.GetAsync(id);

            this.Route.Stops.ForEach((stop) => stop.PropertyChanged += Stop_PropertyChanged);

            Title = Route?.Name;
        }

        async Task ExecuteLoadStopsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Route = await CampDataStore.ListSubscribe();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}