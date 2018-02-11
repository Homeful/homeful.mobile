//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Homeful.mobile
//{
//    // items
//    #region items
//    public class ItemMockDataStore : IDataStore<Item>
//    {
//        List<Item> items;
//        List<Camp> camps;

//        public ItemMockDataStore()
//        {
//            // items
//            items = new List<Item>();
//            var mockItems = new List<Item>
//            {
//                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
//            };

//            foreach (var item in mockItems)
//            {
//                items.Add(item);
//            }

//            // camps
//            camps = new List<Camp>();
//            var mockCamps = new List<Camp>
//            {
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "First camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Second camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Third camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Fourth camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Fifth camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Sixth camp" },
//            };

//            foreach (var camp in mockCamps)
//            {
//                camps.Add(camp);
//            }
//        }
//        public async Task<bool> AddAsync(Item item)
//        {
//            items.Add(item);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> UpdateAsync(Item item)
//        {
//            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
//            items.Remove(_item);
//            items.Add(item);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> DeleteAsync(string id)
//        {
//            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
//            items.Remove(_item);

//            return await Task.FromResult(true);
//        }

//        public async Task<Item> GetAsync(string id)
//        {
//            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
//        }

//        public async Task<IEnumerable<Item>> ListAsync(bool forceRefresh = false)
//        {
//            return await Task.FromResult(items);
//        }
//        #endregion
//    }

//    // camps
//    #region camps
//    public class CampMockDataStore : IDataStore<Camp>
//    {
//        List<Camp> camps;

//        public CampMockDataStore()
//        {
//            // camps
//            camps = new List<Camp>();
//            var mockCamps = new List<Camp>
//            {
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "First camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Second camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Third camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Fourth camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Fifth camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Sixth camp" },
//            };

//            foreach (var camp in mockCamps)
//            {
//                camps.Add(camp);
//            }
//        }
//        public async Task<bool> AddAsync(Camp camp)
//        {
//            camps.Add(camp);

//            return await Task.FromResult(true);
//        }
//        public async Task<bool> UpdateAsync(Camp camp)
//        {
//            var _camp = camps.Where((Camp arg) => arg.Id == camp.Id).FirstOrDefault();
//            camps.Remove(_camp);
//            camps.Add(camp);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> DeleteAsync(string id)
//        {
//            var _camp = camps.Where((Camp arg) => arg.Id == id).FirstOrDefault();
//            camps.Remove(_camp);

//            return await Task.FromResult(true);
//        }

//        public async Task<Camp> GetAsync(string id)
//        {
//            return await Task.FromResult(camps.FirstOrDefault(s => s.Id == id));
//        }

//        public async Task<IEnumerable<Camp>> ListAsync(bool forceRefresh = false)
//        {
//            return await Task.FromResult(camps);
//        }
//        #endregion
//    }

//    // routes
//    #region routes
//    public class RouteMockDataStore : IDataStore<Route>
//    {
//        List<Route> routes;
//        List<Camp> camps;

//        public RouteMockDataStore()
//        {

//            // camps
//            camps = new List<Camp>();
//            var mockCamps = new List<Camp>
//            {
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "First camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Second camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Third camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Fourth camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Fifth camp" },
//                new Camp { Id = Guid.NewGuid().ToString(), Name = "Sixth camp" },
//            };

//            foreach (var camp in mockCamps)
//            {
//                camps.Add(camp);
//            }
//        }
//        public async Task<bool> AddAsync(Route route)
//        {
//            routes.Add(route);

//            return await Task.FromResult(true);
//        }
//        public async Task<bool> UpdateAsync(Route route)
//        {
//            var _route = routes.Where((Route arg) => arg.Id == route.Id).FirstOrDefault();
//            routes.Remove(_route);
//            routes.Add(route);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> DeleteAsync(string id)
//        {
//            var _route = routes.Where((Route arg) => arg.Id == id).FirstOrDefault();
//            routes.Remove(_route);

//            return await Task.FromResult(true);
//        }

//        public async Task<Route> GetAsync(string id)
//        {
//            return await Task.FromResult(routes.FirstOrDefault(s => s.Id == id));
//        }

//        public async Task<IEnumerable<Route>> ListAsync(bool forceRefresh = false)
//        {
//            return await Task.FromResult(routes);
//        }
//        #endregion
//    }
//}
