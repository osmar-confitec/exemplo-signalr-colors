
using Colors.WindownsForms.Enum;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilil.Models;

namespace Colors.WindownsForms
{
    public partial class Form1 : Form
    {

        public IHubProxy HubProxy { get; set; }
        public HubConnection Connection { get; set; }
        const string ServerURI = "http://localhost:9022";
        public ParametrosEntrada ParametrosEntrada { get; set; }

        public Form1(ParametrosEntrada parametrosEntrada)
        {


         

            InitializeComponent();

        
            InicializarHubs();
            PopularFormulario(parametrosEntrada);
            ResetCores();

        




        }

        void ResetCores()
        {

            btnAzul.BackColor = SystemColors.Control;
            btnAzul.ForeColor = Color.Black;


            btnAmarelo.BackColor = SystemColors.Control;
            btnAmarelo.ForeColor = Color.Black;

            btnVerde.BackColor = SystemColors.Control;
            btnVerde.ForeColor = Color.Black;

            btnVermelho.BackColor = SystemColors.Control;
            btnVermelho.ForeColor = Color.Black;

          
        }

        private  async void InicializarHubs()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;
           //stockTickerHub.On( escrita sem passagem nunhuma de parametros 
            HubProxy = Connection.CreateHubProxy("MyHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On("LimparCores", () =>
                  this.Invoke((Action)(() =>
                  {
                      ResetCores();
                  }
               ))
              );
            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException except)
            {

                MessageBox.Show($"  Sem conecção {except.Message} ");
                // StatusText.Text = "Unable to connect to server: Start server before connecting clients.";
                //No connection: Don't enable Send button or show chat UI
                return;
            }


        }

        private void Connection_Closed()
        {
            MessageBox.Show($"  Conecção Encerrada  ");
        }

        private void PopularFormulario(ParametrosEntrada parametrosEntrada)
        {
            ParametrosEntrada = parametrosEntrada;
        }

        public Form1()
        {
            InitializeComponent();
        }

        async void PintarCorAsync(Cores cor)
        {
            switch (cor)
            {
                case Cores.Verde:
                    ParametrosEntrada.Cor = "Verde";
                    await HubProxy.Invoke("EnviarCorSelecionada", ParametrosEntrada);

                    btnVerde.BackColor = Color.LawnGreen;
                    btnVerde.ForeColor = Color.White;
                    break;
                case Cores.Amarelo:

                    ParametrosEntrada.Cor = "Amarelo";
                    await HubProxy.Invoke("EnviarCorSelecionada", ParametrosEntrada);


                    btnAmarelo.BackColor = Color.YellowGreen;
                    btnAmarelo.ForeColor = Color.White;
                    break;
                case Cores.Vermelho:

                    ParametrosEntrada.Cor = "Vermelho";
                    await HubProxy.Invoke("EnviarCorSelecionada", ParametrosEntrada);

                    btnVermelho.BackColor = Color.Red;
                    btnVermelho.ForeColor = Color.White;
                    break;
                case Cores.Azul:

                    ParametrosEntrada.Cor = "Azul";
                    await HubProxy.Invoke("EnviarCorSelecionada", ParametrosEntrada);

                    btnAzul.BackColor = Color.Blue;
                    btnAzul.ForeColor = Color.White;
                    break;
                default:
                    ResetCores();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // pintar cor azul 
            PintarCorAsync(Cores.Azul);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }

        private void btnAmarelo_Click(object sender, EventArgs e)
        {
            PintarCorAsync(Cores.Amarelo);
        }

        private void btnVerde_Click(object sender, EventArgs e)
        {
            PintarCorAsync(Cores.Verde);
        }

        private void btnVermelho_Click(object sender, EventArgs e)
        {
            PintarCorAsync(Cores.Vermelho);

        }
    }
}
