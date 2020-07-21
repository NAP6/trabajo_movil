using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Examen2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Examen2.Services
{
    public class MockDataStore : IDataStore<ItemsListaInver>
    {
        readonly static List<ItemsListaInver> items = new List<ItemsListaInver>();

        public MockDataStore()
        {

        }

        public static async Task<ItemsListaInver> CargaInvernaderos(string usuario)
        {
            var conexion = $"http://192.168.1.5:50980/api/Cliente?username=nicolas";
            using (var inver = new HttpClient())
            {
                var peticion = await inver.GetAsync(conexion);
                if (peticion != null)
                {
                    var content = peticion.Content.ReadAsStringAsync().Result;
                    var json = String.Join("", System.Text.RegularExpressions.Regex.Split(content, @"(?:\\r\\n|\\n|\\r)"));

                    json = json.Replace("\\", "");
                    Debug.WriteLine(json);
                    json = json.Substring(1, json.Length - 2);
                    Debug.WriteLine(json);
                    try
                    {

                        var datos = (JContainer)JsonConvert.DeserializeObject(json);
                        //,new JsonSerializerSettings { NullValueHandling=NullValueHandling.Ignore
                        Debug.WriteLine(datos);

                        items.Clear();
                        foreach (var item in datos)
                        {
                            items.Add(new ItemsListaInver { Id = (string)item["Id"], Nombre = (string)item["Nombre"], Descripcion = (string)item["Descripcion"] });

                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());


                        throw;
                    }
                }
            }
            return default(ItemsListaInver);
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
            await CargaInvernaderos("nicolas");
            return await Task.FromResult(items);
        }
    }
}