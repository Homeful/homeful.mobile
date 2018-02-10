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
                    break;
            }

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
