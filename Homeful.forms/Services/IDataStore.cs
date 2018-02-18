using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;

namespace Homeful.mobile
{
    public interface IDataStore<T>
    {
        Task<FirebaseObject<T>> AddAsync(T item, string path = null);
        Task UpdateAsync(FirebaseObject<T> item, string path = null);
        Task UpdateAsync(T item, string path = null);
        Task DeleteAsync(FirebaseObject<T> id);
        Task<T> GetAsync(string id);
        Task<FirebaseObject<T>> GetObjectAsync(string id);
        Task<IEnumerable<FirebaseObject<T>>> ListAsync(bool forceRefresh = false, string path = null);
        Task<ObservableCollection<FirebaseObject<T>>> ListSubscribe(string path = null);
    }
}
