using System;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class NewRoutePage : ContentPage
    {
        public Route Route { get; set; }

        public NewRoutePage()
        {
            InitializeComponent();

            Route = new Route
            {
                Name = "Route name"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddRoute", Route);
            await Navigation.PopToRootAsync();
        }
    }
}
