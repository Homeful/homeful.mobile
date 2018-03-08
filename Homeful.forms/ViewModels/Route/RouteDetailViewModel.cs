using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Homeful.mobile
{
    public class RouteDetailViewModel : RouteBaseViewModel
    {
        public Route Route { get; set; }
        public Command LoadStopsCommand { get; set; }
        public ObservableCollection<FirebaseObject<Stop>> Stops { get; set; }

        public RouteDetailViewModel(Route route = null)
        {
            Title = route?.Name;
            Route = route;
            Stops = new ObservableCollection<FirebaseObject<Stop>>();
            LoadStopsCommand = new Command(async () => await ExecuteLoadStopsCommand());
        }

        public RouteDetailViewModel(string id)
        {
            LoadData(id);
            Stops = new ObservableCollection<FirebaseObject<Stop>>();
            LoadStopsCommand = new Command(async () => await ExecuteLoadStopsCommand());
        }

        public async void UpdateStop(FirebaseObject<Stop> stop)
        {
            await StopDataStore.UpdateAsync(stop.Object, $"routes/{Route.Id}/Stops/{stop.Key}");
        }
        async void Stop_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var stop = sender as FirebaseObject<Stop>;
            await StopDataStore.UpdateAsync(stop, $"routes/{Route.Id}/Stops/{stop.Key}");
            //await RouteDataStore.UpdateAsync(Route);
        }

        private async void LoadData(string id)
        {
            Route = await RouteDataStore.GetAsync(id);
            this.Route.Stops.ForEach((stop) => stop.Value.PropertyChanged += Stop_PropertyChanged);
            Title = Route?.Name;
        }

        public async void RemoveStop(FirebaseObject<Stop> stop)
        {
            Stops.Remove(stop);
            await StopDataStore.DeleteAsync($"routes/{Route.Id}/Stops/{stop.Key}");
        }

        async Task ExecuteLoadStopsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Stops = await StopDataStore.ListSubscribe($"routes/{Route.Id}/Stops");
                Stops.ToList().ForEach((stop) =>
                {
                    stop.Object.Id = stop.Key;
                    //stop.Object.PropertyChanged += Stop_PropertyChanged;
                });
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