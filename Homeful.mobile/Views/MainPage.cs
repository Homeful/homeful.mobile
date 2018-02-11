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
            Page loginPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    campsPage = new NavigationPage(new CampsPage())
                    {
                        Title = "Browse Camps"
                    };
                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    loginPage = new NavigationPage(new LoginView())
                    {
                        Title = "Login"
                    };

                    campsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    campsPage = new CampsPage()
                    {
                        Title = "Camps"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };

                    loginPage = new LoginView()
                    {
                        Title = "Login"
                    };

                    break;
            }

            Children.Add(campsPage);
            Children.Add(aboutPage);
            Children.Add(loginPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
