using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public class AboutViewModel : ItemBaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}
