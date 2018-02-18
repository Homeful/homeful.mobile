using System;
using System.Linq;
using System.Collections.Generic;
using Firebase.Database;
namespace Homeful.mobile
{
    public class NewRouteViewModel
    {
        public SelectCampsViewModel SelectCampsViewModel { get; set; }

        public Route Route { get; set; }

        public NewRouteViewModel()
        {
            SelectCampsViewModel = new SelectCampsViewModel();
            Route = new Route();
            Route.Stops = new Dictionary<string, Stop>();
            Route.Name = DateTime.Now.ToString("MM/dd/yyyy");
        }

        public void OnCampSelected(SelectCampViewModel camp)
        {
            if (CampIsInRoute(camp))
            {
                RemoveCampFromRoute(camp);
            }
            else
            {
                AddCampToRoute(camp);
            }
        }

        private void AddCampToRoute(SelectCampViewModel camp)
        {
            camp.Selected = true;
            camp.Camp.Object.Id = camp.Camp.Key;
            var stop = new Stop()
            {
                Complete = false,
                Camp = camp.Camp.Object
            };
            Route.Stops.Add(Guid.NewGuid().ToString(), stop);
        }
        private void RemoveCampFromRoute(SelectCampViewModel camp)
        {
            camp.Selected = false;
            // TODO: use Id instead of Name - Id is null currently
            var stop = Route.Stops.FirstOrDefault(s => s.Value.Camp.Id == camp.Camp.Key);
            if (stop.Value != null)
            {
                Route.Stops.Remove(stop.Key);
            }
        }
        private bool CampIsInRoute(SelectCampViewModel camp)
        {
            // TODO: use Id instead of Name - Id is null currently
            return Route.Stops.ToList().Exists(s => s.Value.Camp.Name == camp.Camp.Object.Name);
        }
    }
}