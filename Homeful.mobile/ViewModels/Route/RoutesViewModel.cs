using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public class RoutesViewModel : RouteBaseViewModel
    {
        public ObservableCollection<Route> Routes { get; set; }
        public Command LoadRoutesCommand { get; set; }

        public RoutesViewModel()
        {
            Title = "Browse";
            Routes = new ObservableCollection<Route>();
            LoadRoutesCommand = new Command(async () => await ExecuteLoadRoutesCommand());

            MessagingCenter.Subscribe<NewRoutePage, Route>(this, "AddRoute", async (obj, route) =>
            {
                var _route = route as Route;
                Routes.Add(_route);
                await RouteDataStore.AddAsync(_route);
            });
        }

        async Task ExecuteLoadRoutesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Routes.Clear();
                var routes = await RouteDataStore.GetAsync(true);
                foreach (var route in routes)
                {
                    Routes.Add(route);
                }
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
