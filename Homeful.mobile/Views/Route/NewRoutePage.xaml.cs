using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class NewRoutePage : ContentPage
    {
        public NewRouteViewModel viewModel { get; set; }

        public NewRoutePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NewRouteViewModel();

            viewModel.Route.Name = DateTime.Now.ToString("MM/dd/yyyy");
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddRoute", viewModel.Route);
            await Navigation.PopToRootAsync();
        }

        void OnCampSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var camp = args.SelectedItem as Camp;
            if (camp == null)
                return;

            viewModel.OnCampSelected(camp);

            // Manually deselect camp
            CampsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.CampSelection.Camps.Count == 0)
                viewModel.CampSelection.LoadCampsCommand.Execute(null);
        }
    }
}
