using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen2.Views.L
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvernaderoMasterDetMaster : ContentPage
    {
        List<InvernaderoMasterDetMasterMenuItem> menuItems;

        public InvernaderoMasterDetMaster(int n)
        {
            InitializeComponent();


            menuItems = new List<InvernaderoMasterDetMasterMenuItem>
            {
                new InvernaderoMasterDetMasterMenuItem {Id = MenuItem.mqtt, Title="Tiempo Real" },
                new InvernaderoMasterDetMasterMenuItem {Id = MenuItem.historial, Title="Historial"}
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((InvernaderoMasterDetMasterMenuItem)e.SelectedItem).Id;
                InvernaderoMasterDet RootPage = (InvernaderoMasterDet) Application.Current.MainPage.Navigation.NavigationStack[n];
                if (RootPage != null) { 
                    await RootPage.NavigateFromMenu(id);
                }
                else
                {    
                    Debug.WriteLine("Es null la pagina");
                }
            };
        }

    }
}