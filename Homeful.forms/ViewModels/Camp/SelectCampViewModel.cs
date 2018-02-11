using System;
using Firebase.Database;
using Xamarin.Forms;

namespace Homeful.mobile
{
    public class SelectCampViewModel : CampBaseViewModel
    {
        public FirebaseObject<Camp> Camp { get; set; }

        private bool _selected;
        public bool Selected
        {
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