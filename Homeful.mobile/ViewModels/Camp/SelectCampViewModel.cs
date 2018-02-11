using System;
using Xamarin.Forms;

namespace Homeful.mobile
{
    public class SelectCampViewModel : CampBaseViewModel
    {
        public Camp Camp { get; set; }

        private bool _selected;
        public bool Selected { 
            get 
            {
                return _selected;    
            }
            set
            {
                SetProperty(ref _selected, value);
            }
        }

    }
}
