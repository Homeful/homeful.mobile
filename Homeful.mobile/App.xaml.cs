using System;
using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "https://homeful-d9f3c.firebaseio.com/";

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
            {
                DependencyService.Register<ItemMockDataStore>();
                DependencyService.Register<CampMockDataStore>();
                DependencyService.Register<RouteMockDataStore>();
            }
            else
            {
                //DependencyService.Register<ItemCloudDataStore>();
                DependencyService.Register<CampDataStore>();
                //DependencyService.Register<RouteCloudDataStore>();
            }

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());
        }
    }
}
