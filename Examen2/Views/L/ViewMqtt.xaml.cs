using Examen2.Models;
using M2Mqtt;
using M2Mqtt.Messages;
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

        public ItemsListaInver item { get; set; }
        public ViewMqtt(ItemsListaInver item)
        {
            BindingContext = this.item = item;
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
                string[] topic = { "sensor/temp"};

                byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE};
                cliente.Subscribe(topic, qosLevels);

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
            var topic = e.Topic;
            string msg = e.Message.ToString();

            string[] ambiente = msg.Split(':');
            ambiente = ambiente[0].Split('/');

            string[] temperatura    = ambiente[0].Split('-');
            string[] CO2            = ambiente[0].Split('-');
            string[] humedad_suelo  = ambiente[0].Split('-');
            string[] humedad_aire   = ambiente[0].Split('-');
        }
    }
}

      
    