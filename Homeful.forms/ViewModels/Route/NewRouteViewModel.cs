using System;
using System.Linq;
using System.Collections.Generic;
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
                Camp = camp.Camp.Object,
                Id = camp.Camp.Object.Id
            };
            Route.Stops.Add(stop);
        }
        private void RemoveCampFromRoute(SelectCampViewModel camp)
        {
            camp.Selected = false;
            // TODO: use Id instead of Name - Id is null currently
            var stop = Route.Stops.FirstOrDefault(s => s.Camp.Id == camp.Camp.Key);
            if (stop != null)
            {
                Route.Stops.Remove(stop);
            }
        }
        private bool CampIsInRoute(SelectCampViewModel camp)
        {
            // TODO: use Id instead of Name - Id is null currently
            return Route.Stops.Exists(s => s.Camp.Name == camp.Camp.Object.Name);
        }
    }
}