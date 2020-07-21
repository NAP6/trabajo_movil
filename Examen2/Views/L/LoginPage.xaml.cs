using Examen2.Models;
using Examen2.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
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
            var conexion = $"http://192.168.1.5:50980/api/Cliente?username={usu}&password={con}";
            using (var user = new HttpClient())
            {
                var peticion = await user.GetAsync(conexion);
                if (peticion != null)
                {
                    var content = peticion.Content.ReadAsStringAsync().Result;
                    var json = String.Join("", System.Text.RegularExpressions.Regex.Split(content, @"(?:\\r\\n|\\n|\\r)"));
                    json = json.Replace("\\", "");
                    json = json.Substring(1, json.Length - 2);
                    try
                    {
                            var datos = (JContainer)JsonConvert.DeserializeObject(json);
                            Debug.WriteLine(datos["Existe_usuario"]);
                            Debug.WriteLine((bool)datos["Existe_usuario"]);
                            Debug.WriteLine((string)datos["Existe_usuario"]);
                            if ((bool)datos["Existe_usuario"])
                        {
                            App.usuario = usu;
                            App.contrasenia = con;

                            await Navigation.PushAsync(new Invernaderos());
                        }
                        else
                        {
                            Debug.WriteLine("Esta puteria se jecuta una sola vez");
                            await DisplayAlert("Error", "El usuario No existe", "Ok");
                        }
                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine(ex.Message);
                    }



                }


            }
        }

        private async void Registro_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Examen2.Views.L.SignUpPage());
            
        }

     
    }
}