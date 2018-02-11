using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homeful.mobile.ViewModels
{
    public class LoginViewModel
    {
        public ICommand Login { get; set; }
        public ICommand Signup { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private ILoginService _loginService;
        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            Login = new Command(login);
            Signup = new Command(signup);
        }

        private void login() 
        {
            _loginService.Login(Username, Password);
        }

        private void signup()
        {
            _loginService.Signup(Username, Password);
        }
    }
}
