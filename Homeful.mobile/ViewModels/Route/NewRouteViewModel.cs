using System;
using System.Linq;
using System.Collections.Generic;
namespace Homeful.mobile
{
    public class NewRouteViewModel
    {
        public CampsViewModel CampSelection;
        public Route Route { get; set; }

        public NewRouteViewModel()
        {
            CampSelection = new CampsViewModel();
            Route = new Route();
            Route.Name = DateTime.Now.ToString("MM/dd/yyyy");
        }

        public void OnCampSelected(Camp camp)
        {
            if (CampIsInRoute(camp))
            {
                AddCampToRoute(camp);
            }
            else
            {
                RemoveCampFromRoute(camp);
            }
        }

        private void AddCampToRoute(Camp camp)
        {
            var stop = new Stop()
            {
                Complete = false,
                Camp = camp
            };
            Route.Stops.Add(stop);
        }
        private void RemoveCampFromRoute(Camp camp)
        {
            var stop = Route.Stops.FirstOrDefault(s => s.Camp.Id == camp.Id);
            if (stop != null)
            {
                Route.Stops.Remove(stop);
            }
        }
        private bool CampIsInRoute(Camp camp)
        {
            return Route.Stops.Exists(s => s.Camp.Id == camp.Id);
        }
    }
}
