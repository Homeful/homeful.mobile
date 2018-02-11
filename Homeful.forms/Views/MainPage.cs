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
                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    routesPage.Icon = "tab_routes.png";
                    campsPage.Icon = "tab_camps.png";
                    aboutPage.Icon = "tab_about.png";
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
                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };
                    break;
            }
            Children.Add(routesPage);
            Children.Add(campsPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
