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
            await Navigation.PushAsync(new Invernaderos());
        }

        private async void Registro_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Examen2.Views.L.SignUpPage());
           

        }

     
    }
}