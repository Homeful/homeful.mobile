using System;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page campsPage = null;
            Page aboutPage = null;
            Page routesPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    routesPage = new NavigationPage(new RoutesPage())
                    {
                        Title = "Routes"
                    };
                    campsPage = new NavigationPage(new CampsPage())
                    {
                        Title = "Camps"
                    };
                    routesPage.Icon = "tab_routes.png";
                    campsPage.Icon = "tab_camps.png";
                    break;
                default:
                    routesPage = new RoutesPage()
                    {
                        Title = "Routes"
                    };
                    campsPage = new CampsPage()
                    {
                        Title = "Camps"
                    };
                    break;
            }
            Children.Add(routesPage);
            Children.Add(campsPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
