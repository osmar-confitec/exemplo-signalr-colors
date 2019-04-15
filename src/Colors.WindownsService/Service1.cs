using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Linq;
using System.ServiceProcess;
using Utilil.Models;



[assembly: OwinStartup(typeof(Colors.Startup))]
namespace Colors
{
    public partial class Service1 : ServiceBase
    {

        IDisposable SignalR;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            InicializarServico();
        }

        public void InicializarServico()
        {
            if (!System.Diagnostics.EventLog.SourceExists("SignalRChat"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "SignalRChat", "Application");
            }
            eventLog1.Source = "SignalRChat";
            eventLog1.Log = "Application";

            eventLog1.WriteEntry("In OnStart");
            string url = "http://localhost:9022";
            SignalR = WebApp.Start(url);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop");
            SignalR.Dispose();
        }

      
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

   // [HubName("ColorsHub")]
    public class MyHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.addMessage(message);
        }

        public void InformarPaginaDedoCapturado(string Mensagem, string IdCliente)
        {
            Clients.Client(IdCliente).EnviarPaginaDedoCapturado(Mensagem);
        }

        public void EnviarCorSelecionada(string IdCliente, string Cor)
        {
            Clients.Client(IdCliente).EnviarCorSelecionada(Cor);
        }

        public void EnviarCorSelecionada(ParametrosEntrada parametrosEntrada)
        {
            Clients.Client(parametrosEntrada.IdSignalR).RecebeCor(parametrosEntrada.Cor);
          //  Clients.All.RecebeCor(parametrosEntrada.Cor);
        }

        public void LimparCores()
        {
            Clients.All.LimparCores();
        }

        public void EnviarDedo(string IdCliente, string Hash)
        {
            Clients.Client(IdCliente).EnviarDedo(Hash);
        }

    }
}
