using Examen2.Models;
using M2Mqtt;
using M2Mqtt.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen2.Views.L
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMqtt : ContentPage
    {

        private string direccion = "192.168.1.34";
        private int puerto = 1880;
        private string username = App.usuario;
        private string password = App.contrasenia;
        private MqttClient cliente;

        public ambi_aux ambiente { get; set; }

        public ViewMqtt(ItemsListaInver item)
        {
            ambiente = new ambi_aux();
            ambiente.item = item;
            BindingContext = ambiente;
            InitializeComponent();
            conectar();
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
                string[] topic = { username + "/" + ambiente.item.Id};

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

        private async void Cliente_MqttMsgPublishReceived(object sender, M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var topic = e.Topic;
            string msg = Encoding.UTF8.GetString(e.Message);

            string[] ambiente = msg.Split(':');
            ambiente = ambiente[0].Split('/');

            string[] temperatura    = ambiente[0].Split('-');
            string[] CO2            = ambiente[1].Split('-');
            string[] humedad_suelo  = ambiente[2].Split('-');
            string[] humedad_aire   = ambiente[3].Split('-');

            this.ambiente.Temperatura = temperatura[0];
            this.ambiente.CO2 = CO2[0];
            this.ambiente.Hum_Suel = humedad_suelo[0];
            this.ambiente.Hum_Air = humedad_aire[0];
            
        }

        private async void btn_Calentar_Click(object sender, EventArgs e)
        {
            ushort msgId = cliente.Publish(username + "/" + ambiente.item.Id+ "/actuador/invernadero/calentar", Encoding.UTF8.GetBytes("Calientate oe"),MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,false);
            Debug.WriteLine(msgId);
            try
            {
                await DisplayAlert("MEnsaje enviado", "Calentando", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void btn_Enfriar_Click(object sender, EventArgs e)
        {
            ushort msgId = cliente.Publish(username + "/" + ambiente.item.Id + "/actuador/invernadero/enfriar", Encoding.UTF8.GetBytes("Hace mucho calor prende el aire acondicionado"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            Debug.WriteLine(msgId);
            try
            {
                await DisplayAlert("MEnsaje enviado", "Enfriando", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void btn_Regar_Click(object sender, EventArgs e)
        {
            ushort msgId = cliente.Publish(username + "/" + ambiente.item.Id + "/actuador/invernadero/regar", Encoding.UTF8.GetBytes("Dar de comer a las plantas"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            Debug.WriteLine(msgId);
            try
            {
                await DisplayAlert("MEnsaje enviado", "Regando", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class ambi_aux : INotifyPropertyChanged
    {

        public ItemsListaInver item { get; set; }

        string temperatura = "21";
        public string Temperatura
        {
            get { return temperatura; }
            set { SetProperty(ref temperatura, value); }
        }
        string co2 = "150";
        public string CO2
        {
            get { return co2; }
            set { SetProperty(ref co2, value); }
        }
        string humedad_aire = "20";
        public string Hum_Air
        {
            get { return humedad_aire; }
            set { SetProperty(ref humedad_aire, value); }
        }
        string humedad_suelo = "50";
        public string Hum_Suel
        {
            get { return humedad_suelo; }
            set { SetProperty(ref humedad_suelo, value); }
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            Debug.WriteLine(value);

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}


      
    