using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace Homeful.mobile
{
    //// items
    //#region items
    //public class ItemCloudDataStore : IDataStore<Item>
    //{
    //    HttpClient client;
    //    IEnumerable<Item> items;

    //    public ItemCloudDataStore()
    //    {
    //        client = new HttpClient();
    //        client.BaseAddress = new Uri($"{App.BackendUrl}/");

    //        items = new List<Item>();
    //    }

    //    public async Task<IEnumerable<Item>> GetAsync(bool forceRefresh = false)
    //    {
    //        if (forceRefresh && CrossConnectivity.Current.IsConnected)
    //        {
    //            var json = await client.GetStringAsync($"api/item");
    //            items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
    //        }

    //        return items;
    //    }

    //    public async Task<Item> GetAsync(string id)
    //    {
    //        if (id != null && CrossConnectivity.Current.IsConnected)
    //        {
    //            var json = await client.GetStringAsync($"api/item/{id}");
    //            return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
    //        }

    //        return null;
    //    }

    //    public async Task<bool> AddAsync(Item item)
    //    {
    //        if (item == null || !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var serializedItem = JsonConvert.SerializeObject(item);

    //        var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

    //        return response.IsSuccessStatusCode;
    //    }

    //    public async Task<bool> UpdateAsync(Item item)
    //    {
    //        if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var serializedItem = JsonConvert.SerializeObject(item);
    //        var buffer = Encoding.UTF8.GetBytes(serializedItem);
    //        var byteContent = new ByteArrayContent(buffer);

    //        var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

    //        return response.IsSuccessStatusCode;
    //    }

    //    public async Task<bool> DeleteAsync(string id)
    //    {
    //        if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var response = await client.DeleteAsync($"api/item/{id}");

    //        return response.IsSuccessStatusCode;
    //    }
    //    #endregion
    //}

    //// camps
    //#region camps
    //public class CampCloudDataStore : IDataStore<Camp>
    //{
    //    HttpClient client;
    //    IEnumerable<Camp> camps;

    //    public CampCloudDataStore()
    //    {
    //        client = new HttpClient();
    //        client.BaseAddress = new Uri($"{App.BackendUrl}/");

    //        camps = new List<Camp>();
    //    }
    //    public async Task<IEnumerable<Camp>> GetAsync(bool forceRefresh = false)
    //    {
    //        if (forceRefresh && CrossConnectivity.Current.IsConnected)
    //        {
    //            var json = await client.GetStringAsync($"api/camp");
    //            camps = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Camp>>(json));
    //        }

    //        return camps;
    //    }

    //    public async Task<Camp> GetAsync(string id)
    //    {
    //        if (id != null && CrossConnectivity.Current.IsConnected)
    //        {
    //            var json = await client.GetStringAsync($"api/camp/{id}");
    //            return await Task.Run(() => JsonConvert.DeserializeObject<Camp>(json));
    //        }

    //        return null;
    //    }

    //    public async Task<bool> AddAsync(Camp camp)
    //    {
    //        if (camp == null || !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var serializedCamp = JsonConvert.SerializeObject(camp);

    //        var response = await client.PostAsync($"api/camp", new StringContent(serializedCamp, Encoding.UTF8, "application/json"));

    //        return response.IsSuccessStatusCode;
    //    }

    //    public async Task<bool> UpdateAsync(Camp camp)
    //    {
    //        if (camp == null || camp.Id == null || !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var serializedCamp = JsonConvert.SerializeObject(camp);
    //        var buffer = Encoding.UTF8.GetBytes(serializedCamp);
    //        var byteContent = new ByteArrayContent(buffer);

    //        var response = await client.PutAsync(new Uri($"api/camp/{camp.Id}"), byteContent);

    //        return response.IsSuccessStatusCode;
    //    }

    //    public async Task<bool> DeleteAsync(string id)
    //    {
    //        if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var response = await client.DeleteAsync($"api/camp/{id}");

    //        return response.IsSuccessStatusCode;
    //    }
    //    #endregion
    //}

    //// routes
    //#region routes
    //public class RouteCloudDataStore : IDataStore<Route>
    //{
    //    HttpClient client;
    //    IEnumerable<Route> routes;

    //    public RouteCloudDataStore()
    //    {
    //        client = new HttpClient();
    //        client.BaseAddress = new Uri($"{App.BackendUrl}/");

    //        routes = new List<Route>();
    //    }

    //    public async Task<IEnumerable<Route>> GetAsync(bool forceRefresh = false)
    //    {
    //        if (forceRefresh && CrossConnectivity.Current.IsConnected)
    //        {
    //            var json = await client.GetStringAsync($"api/route");
    //            routes = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Route>>(json));
    //        }

    //        return routes;
    //    }

    //    public async Task<Route> GetAsync(string id)
    //    {
    //        if (id != null && CrossConnectivity.Current.IsConnected)
    //        {
    //            var json = await client.GetStringAsync($"api/route/{id}");
    //            return await Task.Run(() => JsonConvert.DeserializeObject<Route>(json));
    //        }

    //        return null;
    //    }

    //    public async Task<bool> AddAsync(Route route)
    //    {
    //        if (route == null || !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var serializedRoute = JsonConvert.SerializeObject(route);

    //        var response = await client.PostAsync($"api/route", new StringContent(serializedRoute, Encoding.UTF8, "application/json"));

    //        return response.IsSuccessStatusCode;
    //    }

    //    public async Task<bool> UpdateAsync(Route route)
    //    {
    //        if (route == null || route.Id == null || !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var serializedRoute = JsonConvert.SerializeObject(route);
    //        var buffer = Encoding.UTF8.GetBytes(serializedRoute);
    //        var byteContent = new ByteArrayContent(buffer);

    //        var response = await client.PutAsync(new Uri($"api/route/{route.Id}"), byteContent);

    //        return response.IsSuccessStatusCode;
    //    }

    //    public async Task<bool> DeleteAsync(string id)
    //    {
    //        if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
    //            return false;

    //        var response = await client.DeleteAsync($"api/route/{id}");

    //        return response.IsSuccessStatusCode;
    //    }
    //    #endregion
    //}
}
