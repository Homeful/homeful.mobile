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

namespace Homeful.mobile
{
    // camps
    #region camps
    public abstract class FirebaseDataStore<T> : IDataStore<T> where T : IFirebaseItem
    {
        HttpClient client;
        IEnumerable<T> items;

        protected abstract string Path { get; set; }

        public FirebaseDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            items = new List<T>();
        }
        public async Task<IEnumerable<T>> ListAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"{Path}.json");
                var i = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
                items = i.ToList().Select(kvp =>
                {
                    kvp.Value.Id = kvp.Key;
                    return kvp;
                }).Select(kvp => kvp.Value);
                //items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
            }

            return items;
        }

        public async Task<T> GetAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"{Path}/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }

            return default(T);
        }

        public async Task<bool> AddAsync(T item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            //serializedItem = "{\"name\": \"Test\"}";
            var message = new HttpRequestMessage(new HttpMethod("POST"), $"{Path}.json")
            {
                Content = new StringContent(serializedItem)
            };
            var response = await client.SendAsync(message);
            //var response = await client.PostAsync("${Path}.json", new StringContent(serializedItem, Encoding.ASCII, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(T item)
        {
            if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedCamp = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedCamp);
            var byteContent = new ByteArrayContent(buffer);

            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, new Uri($"{Path}/{item.Id}.json"))
            {
                Content = byteContent
            };

            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"{Path}/{id}.json");

            return response.IsSuccessStatusCode;
        }
        #endregion


        private string Request(string path, string method, object payload = null) 
        {
            
            var request = WebRequest.CreateHttp($"{App.BackendUrl}/{path}");
            request.Method = method;
            request.ContentType = "application/json";
            if (payload != null) 
            {
                var json = JsonConvert.SerializeObject(payload);
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }

            var response = request.GetResponse();
            var responseJson = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return responseJson;
        }
    }

}
