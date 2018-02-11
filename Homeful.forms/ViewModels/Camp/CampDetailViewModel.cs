using System;
using System.Linq;
using System.Windows.Input;
using Firebase.Database;
using Xamarin.Forms;

namespace Homeful.mobile
{
    public class CampDetailViewModel : CampBaseViewModel
    {
        public FirebaseObject<Camp> Camp { get; set; }
        public FirebaseObject<Route> Route { get; set; }

        public ICommand LoadNextCamp { get; set; }

        public CampDetailViewModel(FirebaseObject<Camp> camp = null)
        {
            Title = camp?.Object.Name;
            Camp = camp;
            LoadNextCamp = new Command(LoadNextcamp);

            LoadRoute();

        }

        private async void LoadRoute() 
        {
            RouteDataStore r = DependencyService.Get<RouteDataStore>();
            var routes = await r.ListAsync();
        }   

        public CampDetailViewModel(FirebaseObject<Route> route, FirebaseObject<Camp> currentCamp) : this(currentCamp)
        {
            Route = route;
        }

        private async void LoadNextcamp()
        {
            int index = Route.Object.Stops.IndexOf(Route.Object.Stops.First(s => s.Camp.Id == Camp.Key));
            Camp = await CampDataStore.GetObjectAsync(Route.Object.Stops[index + 1].Camp.Id);
        }
    }
}
