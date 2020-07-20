using Examen2.ViewModels.L;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Examen2.Models;
using System.Diagnostics;

namespace Examen2.Views.L
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Invernaderos : ContentPage
    {
        ItemsListaInverViewModel viewModel;
        public Invernaderos()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new ItemsListaInverViewModel();
            

        }
        public async void OnItemSelected(object sender, EventArgs args)
        {
       
            var layout = (BindableObject)sender;
            var item = (ItemsListaInver)layout.BindingContext;
            InvernaderoMasterDet especifico = new InvernaderoMasterDet(item);
            NavigationPage.SetHasNavigationBar(especifico, false);
            await Navigation.PushAsync(especifico);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}