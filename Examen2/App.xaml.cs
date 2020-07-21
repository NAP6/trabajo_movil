using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Examen2.Services;


namespace Examen2
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public static string usuario { get; set; }
        public static string contrasenia { get; set; }
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<InvernaderoService>();
            MainPage = new NavigationPage(new Examen2.Views.L.LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
