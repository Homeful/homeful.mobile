using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace Homeful.mobile
{
    // camps
    #region camps
    public abstract class FirebaseDataStore<T> : IDataStore<T> where T : IFirebaseItem
    {
        HttpClient client;
        IEnumerable<FirebaseObject<T>> items;

        protected abstract string Path { get; set; }

        const string BACKEND_URL = "https://homeful-d9f3c.firebaseio.com/";
        FirebaseClient firebase;

        public FirebaseDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{BACKEND_URL}/");

            firebase = new FirebaseClient(BACKEND_URL);

            items = new List<FirebaseObject<T>>();
        }

        public async Task<IEnumerable<FirebaseObject<T>>> ListAsync(bool forceRefresh = false)
        {
            if (forceRefresh || items.Count() == 0)
            {
                items = await firebase.Child($"{Path}").OnceAsync<T>();
            }

            return items;
        }

        public async Task<ObservableCollection<FirebaseObject<T>>> ListSubscribe()
        {
            ObservableCollection<FirebaseObject<T>> collection = new ObservableCollection<FirebaseObject<T>>(await ListAsync(true));

            firebase.Child($"{Path}").AsObservable<T>().Subscribe(e =>
            {
                if (e.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                {
                    collection.Remove(collection.First(i => i.Key == e.Key));
                }
                else if (collection.Any(i => i.Key == e.Key))
                {
                    var index = collection.IndexOf(collection.First(i => i.Key == e.Key));
                    collection[index] = e;
                }
                else
                {

                    collection.Add(e);
                }
            });

            return collection;
        }

        public async Task<T> GetAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var obj = await firebase.Child($"{Path}").Child($"{id}").OnceSingleAsync<T>();
                obj.Id = id;
                return obj;
                //var json = await client.GetStringAsync($"{Path}/{id}");
                //return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }

            return default(T);
        }

        public async Task<FirebaseObject<T>> GetObjectAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                return (await firebase.Child($"{Path}").OnceAsync<T>()).First(c => c.Key == id);
            }

            return default(FirebaseObject<T>);
        }

        public async Task<FirebaseObject<T>> AddAsync(T item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return null;

            return await firebase.Child($"{Path}").PostAsync(item);
        }

        public async Task UpdateAsync(FirebaseObject<T> item)
        {
            await firebase.Child($"{Path}").PutAsync(item);
        }

        public async Task UpdateAsync(T item)
        {
            await firebase.Child($"{Path}").Child($"{item.Id}").PutAsync(item);
        }

        public async Task DeleteAsync(FirebaseObject<T> item)
        {
            await firebase.Child($"{Path}").Child($"{item.Key}").DeleteAsync();
        }
        #endregion
    }

}
