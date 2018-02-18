using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace Homeful.mobile
{
    public class RoutesViewModel : RouteBaseViewModel
    {
        public ObservableCollection<FirebaseObject<Route>> Routes { get; set; }
        public Command LoadRoutesCommand { get; set; }

        public RoutesViewModel()
        {
            Title = "Browse";
            Routes = new ObservableCollection<FirebaseObject<Route>>();
            LoadRoutesCommand = new Command(async () => await ExecuteLoadRoutesCommand());

            MessagingCenter.Subscribe<NewRoutePage, Route>(this, "AddRoute", async (obj, route) =>
            {
                var _route = route as Route;
                var _stops = _route.Stops as Dictionary<string, Stop>;
                _route.Stops = null;
                _route.CreatedAt = DateTime.Now;
                _route.ModifiedAt = DateTime.Now;

                var newRoute = await RouteDataStore.AddAsync(_route);
                _stops.ForEach(async stop => await StopDataStore.AddAsync(stop.Value, $"routes/{newRoute.Key}/Stops"));
            });
        }

        async Task ExecuteLoadRoutesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Routes = await RouteDataStore.ListSubscribe();
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