﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Examen2.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Examen2.ViewModels.L
{
     public class ItemsListaInverViewModel:BaseViewModel2
    {
        public ObservableCollection<ItemsListaInver> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsListaInverViewModel()
        {
            Title = "Lista Invernaderos";
            Items = new ObservableCollection<ItemsListaInver>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            /// MessagingCenter.Subscribe<NewItemPage, ItemsListaInver>(this, "AddItem", async (obj, item) =>
            //{
            //  var newItem = item as ItemsListaInver;
            //Items.Add(newItem);
            // await DataStore.AddItemAsync(newItem);
            // });
        }

        async Task ExecuteLoadItemsCommand()
        {
            
           
            IsBusy = true;
            
            try
            {
               
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Debug.Write("***************************** " + Items.Count);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("aQUIIIIIIIII EROOOOOR OYEEE AQUIIII EROOOOR");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
