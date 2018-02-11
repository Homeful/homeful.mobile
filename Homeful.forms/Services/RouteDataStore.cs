using System;
namespace Homeful.mobile
{
    public class RouteDataStore : FirebaseDataStore<Route>
    {
        protected override string Path { get; set; } = "routes";

        public RouteDataStore()
        {
        }
    }
}
