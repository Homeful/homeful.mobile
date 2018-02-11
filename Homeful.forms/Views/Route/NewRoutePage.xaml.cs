using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddRoute", viewModel.Route);
            await Navigation.PopToRootAsync();
        }

        void OnCampSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var camp = args.SelectedItem as SelectCampViewModel;
            if (camp == null)
                return;

            viewModel.OnCampSelected(camp);

            // Manually deselect camp
            CampsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.SelectCampsViewModel.SelectCamps.Count() == 0)
                viewModel.SelectCampsViewModel.LoadCampsCommand.Execute(null);
        }
    }
}