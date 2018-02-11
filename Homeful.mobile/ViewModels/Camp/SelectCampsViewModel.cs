using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Diagnostics;

namespace Homeful.mobile
{
    public class SelectCampsViewModel : CampBaseViewModel
    {
        private IEnumerable<SelectCampViewModel> _selectCamps;
        public IEnumerable<SelectCampViewModel> SelectCamps { 
            get 
            {
                return _selectCamps;
            }
            set 
            {
                SetProperty(ref _selectCamps, value);    
            }
        }

        public ICommand LoadCampsCommand { get; set; }

        public SelectCampsViewModel()
        {
            SelectCamps = new List<SelectCampViewModel>();
            LoadCampsCommand = new Command(async () => await ExecuteLoadCampsCommand());
        }

        async Task ExecuteLoadCampsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                IEnumerable<Camp> camps = await CampDataStore.ListAsync(true);
                SelectCamps = camps.Select(camp => new SelectCampViewModel { Camp = camp, Selected = false });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
