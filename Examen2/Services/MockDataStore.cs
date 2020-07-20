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
                new ItemsListaInver { Id = "100", Nombre = "Invernadero Experimental",Descripcion="Este es un invernadero experimental en chaullabamba." }
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