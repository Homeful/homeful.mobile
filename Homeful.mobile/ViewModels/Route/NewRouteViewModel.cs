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
            var stop = new Stop()
            {
                Complete = false,
                Camp = camp.Camp
            };
            Route.Stops.Add(stop);
        }
        private void RemoveCampFromRoute(SelectCampViewModel camp)
        {
            camp.Selected = false;
            var stop = Route.Stops.FirstOrDefault(s => s.Camp.Id == camp.Camp.Id);
            if (stop != null)
            {
                Route.Stops.Remove(stop);
            }
        }
        private bool CampIsInRoute(SelectCampViewModel camp)
        {
            return Route.Stops.Exists(s => s.Camp.Id == camp.Camp.Id);
        }
    }
}
