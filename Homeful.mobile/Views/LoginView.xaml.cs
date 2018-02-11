using System;
using System.Collections.Generic;
using Homeful.mobile.ViewModels;
using Xamarin.Forms;

namespace Homeful.mobile
{
    public partial class LoginView : ContentPage
    {
        LoginViewModel viewModel;
        public LoginView()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel(DependencyService.Get<ILoginService>());
        }

    }
}
