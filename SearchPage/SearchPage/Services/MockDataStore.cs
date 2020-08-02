using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchPage.Models;

namespace SearchPage.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        //this is second list
        readonly List<Item> tab2;

        public MockDataStore()
        {
            // we use this sample data for search or list.

            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };  // 1st list data

            tab2 = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
               new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            }; // 2nd list data

            // this our data
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        //Get items of Tab 1 list :
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false) // this api is GetAllItems api 
        {
            return await Task.FromResult(items);
        }

        //Get items of Tab 2 list :
        public async Task<IEnumerable<Item>> GetTab2ItemsAsync(bool forceRefresh = false) // this api is GetAllItems api 
        {
            return await Task.FromResult(tab2);
        }

        public async Task<IEnumerable<Item>> GetItemsBySearchKeyword(string searchKeyword, bool forceRefresh = false)
        {
            return await Task.FromResult(items.Where(x => x.Text.ToLower().Contains(searchKeyword.ToLower()))); // and this our api complte
        }
    }
}