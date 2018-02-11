using System;
namespace Homeful.mobile
{
    public class RouteDetailViewModel : RouteBaseViewModel
    {
        public Route Route { get; set; }
        public RouteDetailViewModel(Route route = null)
        {
            Title = route?.Name;
            Route = route;
        }
    }
}