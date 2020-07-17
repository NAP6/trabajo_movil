using M2Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen2.Views.L
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMqtt : ContentPage
    {

        private string direccion = "";
        private int puerto = 1880;
        private string usuario;
        private string username;
        private string password;
        private MqttClient cliente;
        private List<string> suscripcion = new List<string>();

        public ViewMqtt()
        {
            InitializeComponent();
        }
        public void conectar()
        {
            try
            {
                cliente = new MqttClient(direccion, puerto, false, null, null, MqttSslProtocols.TLSv1_2);
                cliente.MqttMsgPublishReceived += Cliente_MqttMsgPublishReceived;
                cliente.ConnectionClosed += Cliente_ConnectionClosed;
                cliente.Connect("Aplicacion",username,password);
                if (!cliente.IsConnected)
                {
                    DisplayAlert("ERROR","Cliente No Conectado","OK");

                }
                byte[] Qos ={0};
                string aux;
                foreach (var item in collection)
                {

                }

            }
            catch (M2Mqtt.Exceptions.MqttClientException) 
            {
                DisplayAlert("ERROR", "Exceptions MQTT", "ok");

                throw;
            }

        }

        private void Cliente_ConnectionClosed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Cliente_MqttMsgPublishReceived(object sender, M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            if (suscripcion.Contains(e.Topic.ToString()))
            {
                String[] separador = { "/" };
                string[] aux = e.Topic.ToString().Split(new char[]  = { '/'});

            }
        }
    }
}

      
    