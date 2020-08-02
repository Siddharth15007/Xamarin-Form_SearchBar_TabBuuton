using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchPage.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);

        // and this is for 1st list
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        // this for 2nd list
        Task<IEnumerable<T>> GetTab2ItemsAsync(bool forceRefresh = false);


        Task<IEnumerable<T>> GetItemsBySearchKeyword(string searchKeyword, bool forceRefresh = false);

    }
}
