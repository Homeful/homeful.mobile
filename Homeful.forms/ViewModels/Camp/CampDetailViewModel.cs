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
        private FirebaseObject<Route> _route = null;
        public FirebaseObject<Route> Route { 
            get 
            {
                return _route;
            }
            set
            {
                SetProperty(ref _route, value);
            }
        }

        public bool RouteAvailable
        {
            get 
            {
                return _route != null;    
            }    
        }

 
        public ICommand LoadNextCamp { get; set; }
        public bool NextAvailable { get; set; } = false;

        public CampDetailViewModel(FirebaseObject<Camp> camp = null)
        {
            Title = camp?.Object.Name;
            Camp = camp;
            LoadNextCamp = new Command(LoadNextcamp);
        }


        public CampDetailViewModel(string routeId, string currentCampId)
        {
            LoadNextCamp = new Command(LoadNextcamp);
            LoadData(routeId, currentCampId);

        }

        private async void LoadData(string routeId, string currentCampId)
        {
            IDataStore<Route> r = DependencyService.Get<IDataStore<Route>>();
            IDataStore<Camp> c = DependencyService.Get<IDataStore<Camp>>();

            Route = await r.GetObjectAsync(routeId); //(await r.ListAsync()).FirstOrDefault(a => a.Key == routeId);
            Camp = await c.GetObjectAsync(currentCampId);

            if(IndexOfCurrentStop() + 1 < Route.Object.Stops.Count())
            {
                NextAvailable = true;
            }
            else 
            {
                NextAvailable = false;    
            }
        }

        private async void LoadNextcamp()
        {
            int index = IndexOfCurrentStop();
            Camp = await CampDataStore.GetObjectAsync(Route.Object.Stops[index + 1].Camp.Id);

            int i = IndexOfCurrentStop();
            if (i + 1 < Route.Object.Stops.Count())
            {
                NextAvailable = true;
            }
            else
            {
                NextAvailable = false;
            }
        }

        private int IndexOfCurrentStop()
        {
            return Route.Object.Stops.IndexOf(Route.Object.Stops.First(s => s.Camp.Id == Camp.Key));
        }
    }
}
