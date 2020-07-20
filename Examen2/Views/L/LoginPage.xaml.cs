using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Examen2.Views.L
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            Registro.Clicked += Registro_Clicked;
            Ingresar.Clicked += Ingresar_Clicked;
            
        }

        private async void Ingresar_Clicked(object sender, System.EventArgs e)
        {
            string usu = NameEntry.Text;
            string con = PasswordEntry.Text;

            if (usu == "nicolas" && con == "nico0506"){
                App.usuario = usu;
                App.contrasenia = con;

                await Navigation.PushAsync(new Invernaderos());
            }
            else
            {
                Debug.WriteLine("Ingresa bien el usuario ycontrasenia no seas idiota");    
            }
        }

        private async void Registro_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Examen2.Views.L.SignUpPage());
           

        }

     
    }
}