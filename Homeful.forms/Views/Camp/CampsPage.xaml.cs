using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class CampsPage : ContentPage
    {
        CampsViewModel viewModel;

        public CampsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CampsViewModel();
        }

        async void OnCampSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var camp = args.SelectedItem as FirebaseObject<Camp>;
            if (camp == null)
                return;

            await Navigation.PushAsync(new CampDetailPage(new CampDetailViewModel(camp)));

            // Manually deselect camp
            CampsListView.SelectedItem = null;
        }

        async void AddCamp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCampPage());
        }

        async void OnRemove(object sender, EventArgs e)
        {
            var camp = ((MenuItem)sender).CommandParameter as FirebaseObject<Camp>;
            await viewModel.Remove(camp);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Camps.Count() == 0)
                viewModel.LoadCampsCommand.Execute(null);
        }
    }
}
