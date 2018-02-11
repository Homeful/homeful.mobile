using System;

using Xamarin.Forms;

namespace Homeful.forms.Views.Route
{
    public class RouteDetailPage : ContentPage
    {
        public RouteDetailPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

