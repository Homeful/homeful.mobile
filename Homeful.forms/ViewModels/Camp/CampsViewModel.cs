using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;


namespace Homeful.mobile
{
    public class CampsViewModel : CampBaseViewModel
    {
        public ObservableCollection<FirebaseObject<Camp>> Camps { get; set; }
        public Command LoadCampsCommand { get; set; }

        public CampsViewModel()
        {
            Title = "Browse";
            Camps = new ObservableCollection<FirebaseObject<Camp>>();
            LoadCampsCommand = new Command(async () => await ExecuteLoadCampsCommand());

            MessagingCenter.Subscribe<NewCampPage, Camp>(this, "AddCamp", async (obj, camp) =>
            {
                var _camp = camp as Camp;
                Camps.Add(await CampDataStore.AddAsync(_camp));
            });
        }

        async Task ExecuteLoadCampsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Camps = await CampDataStore.ListSubscribe();

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
