using System;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class NewCampPage : ContentPage
    {
        public Camp Camp { get; set; }

        public NewCampPage()
        {
            InitializeComponent();

            Camp = new Camp
            {
                Name = "Camp name"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddCamp", Camp);
            await Navigation.PopToRootAsync();
        }
    }
}
