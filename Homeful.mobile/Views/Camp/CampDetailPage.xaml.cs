using System;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class CampDetailPage : ContentPage
    {
        CampDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public CampDetailPage()
        {
            InitializeComponent();

            var camp = new Camp
            {
                Name = "Camp 1"
            };

            viewModel = new CampDetailViewModel(camp);
            BindingContext = viewModel;
        }

        public CampDetailPage(CampDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
