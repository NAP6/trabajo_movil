using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Examen2.Views.L
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();
            Ingreso.Clicked += Ingreso_Clicked;
            
        }

        private async void Ingreso_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Examen2.Views.L.LoginPage());
        }

   
    }
}