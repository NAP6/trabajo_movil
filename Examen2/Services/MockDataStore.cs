using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen2.Models;

namespace Examen2.Services
{
    public class MockDataStore : IDataStore<ItemsListaInver>
    {
        readonly List<ItemsListaInver> items;

        public MockDataStore()
        {
            items = new List<ItemsListaInver>()
            {
                new ItemsListaInver { Id = Guid.NewGuid().ToString(), Nombre = "First item",Descripcion="This is an item description." },
                new ItemsListaInver { Id = Guid.NewGuid().ToString(), Nombre = "Second item", Descripcion="This is an item description." },
                new ItemsListaInver { Id = Guid.NewGuid().ToString(), Nombre = "Third item", Descripcion="This is an item description." },
                new ItemsListaInver { Id = Guid.NewGuid().ToString(), Nombre = "Fourth item", Descripcion="This is an item description." },
                new ItemsListaInver { Id = Guid.NewGuid().ToString(), Nombre = "Fifth item", Descripcion="This is an item description." },
                new ItemsListaInver { Id = Guid.NewGuid().ToString(), Nombre = "Sixth item", Descripcion="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(ItemsListaInver item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ItemsListaInver item)
        {
            var oldItem = items.Where((ItemsListaInver arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ItemsListaInver arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemsListaInver> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemsListaInver>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}