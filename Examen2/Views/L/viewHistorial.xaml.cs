using Examen2.Models;
using Examen2.ViewModels.L;
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
    public partial class viewHistorial : ContentPage
    {
        ambienteViewModels viewModel;
        public viewHistorial(ItemsListaInver item)
        {
            InitializeComponent();
            BindingContext = viewModel = new ambienteViewModels();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}