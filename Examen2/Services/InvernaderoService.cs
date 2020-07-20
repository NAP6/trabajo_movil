using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen2.Models;

namespace Examen2.Services
{
    public class InvernaderoService : IDataStore<ambiente>
    {
        readonly List<ambiente> items;

        public InvernaderoService()
        {
            items = new List<ambiente>()
            {
                new ambiente {fecha = "06/07/2020 08:20:50", temperatura = "21", co2 = "120", humedad_aire = "30",humedad_suelo = "80"},
                new ambiente {fecha = "06/07/2020 08:20:50", temperatura = "21", co2 = "120", humedad_aire = "30",humedad_suelo = "80"},
                new ambiente {fecha = "06/07/2020 08:20:50", temperatura = "21", co2 = "120", humedad_aire = "30",humedad_suelo = "80"},
                new ambiente {fecha = "06/07/2020 08:20:50", temperatura = "21", co2 = "120", humedad_aire = "30",humedad_suelo = "80"}
            };
        }

        public async Task<bool> AddItemAsync(ambiente item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ambiente item)
        {
            var oldItem = items.Where((ambiente arg) => arg.fecha == item.fecha).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ambiente arg) => arg.fecha == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ambiente> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.fecha == id));
        }

        public async Task<IEnumerable<ambiente>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
