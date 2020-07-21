using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Essentials;
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

        private async void Registrarse_Clicked(object sender, System.EventArgs e)
        {
            string contra1 = Password1.Text;
            string contra2 = Password2.Text;
            string username = usuario.Text;
            string correo = correoUser.Text;
           


            if (contra1!=contra2)
            {
                    await DisplayAlert("Error de Credenciales", "Las Contraseñas no coinciden", "OK");
            } else
            {
                var conexion = $"http://192.168.1.5:50980/api/Cliente?username={username}&password={contra1}";
                using (var user = new HttpClient())
                {
                    var peticion = await user.GetAsync(conexion);
                    if (peticion != null)
                    {
                        var content = peticion.Content.ReadAsStringAsync().Result;
                        var json =String.Join("", System.Text.RegularExpressions.Regex.Split(content, @"(?:\\r\\n|\\n|\\r)"));
                        json = json.Replace("\\", "");
                        json = json.Substring(1, json.Length - 2);
                        try
                        {
                            var datos = (JContainer)JsonConvert.DeserializeObject(json);
                            
                            if ((bool)datos["Existe_usuario"])
                            {
                                await DisplayAlert("Error","El usuario ya se encuentra Registrado","Ok");
                            }
                            else
                            {
                                var conexion2 = $"http://192.168.1.5:50980/api/Cliente?username={username}&password={contra1}&correo={correo}";
                                using (var user2 = new HttpClient())
                                {
                                    var peticion2 = await user.GetAsync(conexion2);
                                    if (peticion != null)
                                    {
                                        await DisplayAlert("Correcto","El usuario se registro","Ok");
                                        await Navigation.PushAsync(new Examen2.Views.L.LoginPage());
                                    }
                                }
                                        
                                        
                            }

                        }
                        catch (System.Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            throw;
                        }

                    }


                }
            }

        }
    }
}