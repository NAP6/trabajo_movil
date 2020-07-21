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
    public class InvernaderoService : IDataStore<ambiente>
    {
        readonly static List<ambiente> items = new List<ambiente>();

        public InvernaderoService()
        {
        }

        public static async Task<ambiente> CargaHisto(string invernadero)
        {
            var conexion = $"http://192.168.1.5:50980/api/Historial?invernadero={invernadero}";
            using (var inver = new HttpClient())
            {
                var peticion = await inver.GetAsync(conexion);
                if (peticion != null)
                {
                  
                    var content = peticion.Content.ReadAsStringAsync().Result;
                    var json = String.Join("", System.Text.RegularExpressions.Regex.Split(content, @"(?:\\r\\n|\\n|\\r)"));

                    json = json.Replace("\\","");
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
                            items.Add(new ambiente { fecha = (string)item["Fecha"], temperatura = (string)item["Temperatura"], co2 = (string)item["Co2"], humedad_aire = (string)item["Humedad_aire"], humedad_suelo = (string)item["Humedad_suelo"] });
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                

                        throw;
                    }
                }
            }
            return default( ambiente);
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
            await CargaHisto(App.invernadero);
            return await Task.FromResult(items);
        }
    }
}
