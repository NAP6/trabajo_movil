using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen2.Views.L
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvernaderoMasterDet : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public InvernaderoMasterDet(ItemsListaInver item)
        {
            int index = Application.Current.MainPage.Navigation.NavigationStack.Count;
            Master = new InvernaderoMasterDetMaster(index);

            InitializeComponent();

            

            MenuPages.Add((int)MenuItem.mqtt, (NavigationPage)Detail);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItem.mqtt:
                        MenuPages.Add(id, new NavigationPage(new ViewMqtt()));
                        break;
                    case (int)MenuItem.historial:
                        MenuPages.Add(id, new NavigationPage(new viewHistorial()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Debug.WriteLine("Cambia");
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
    }