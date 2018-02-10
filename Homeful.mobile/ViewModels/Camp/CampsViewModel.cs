using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Homeful.mobile
{
    public class CampsViewModel : CampBaseViewModel
    {
        public ObservableCollection<Camp> Camps { get; set; }
        public Command LoadCampsCommand { get; set; }

        public CampsViewModel()
        {
            Title = "Browse";
            Camps = new ObservableCollection<Camp>();
            LoadCampsCommand = new Command(async () => await ExecuteLoadCampsCommand());

            MessagingCenter.Subscribe<NewCampPage, Camp>(this, "AddCamp", async (obj, camp) =>
            {
                var _camp = camp as Camp;
                Camps.Add(_camp);
                await CampDataStore.AddAsync(_camp);
            });
        }

        async Task ExecuteLoadCampsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Camps.Clear();
                var camps = await CampDataStore.GetAsync(true);
                foreach (var camp in camps)
                {
                    Camps.Add(camp);
                }
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
