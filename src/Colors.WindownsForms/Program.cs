
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Utilil;
using Utilil.Models;

namespace Colors.WindownsForms
{
    static class Program
    {


        static bool AplicacaoAberta()
        {
            Process AppAplicacao = Process.GetCurrentProcess();
            string aProcName = AppAplicacao.ProcessName;
            return (Process.GetProcessesByName(aProcName).Length > 1);
                
        }
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {

        

                ////capturando parametros de entrada para serem lidos 
                string[] args = Environment.GetCommandLineArgs();

                var parEntrada = new ParametrosEntrada();
                ////nome do aplicativo que será invocado
                string nomeAplicacao = ConfigurationManager.AppSettings["aplicativo"];


                //// se há mais de um parametro é porque uma aplicação passou eles  
                if (args.Count() > 1
                    && !string.IsNullOrEmpty(args[1]))
                {
                    if (args[1].ToLower().Contains(nomeAplicacao.ToLower()))
                    {
                        // Objeto tratado do Site com o tratamento 
                        var ObjSite = HttpUtility.UrlDecode(args[1].Remove(args[1].IndexOf(":"), 1).Replace(nomeAplicacao.ToLower(), ""));
                        //Está sendo chamado do site
                        parEntrada = JsonConvert
                              .DeserializeObject<ParametrosEntrada>(ObjSite);

                        //Verifica se a aplicação já está em execução se sim fecha.

                        if (AplicacaoAberta())
                        {
                            Application.Exit();
                            return;
                        }

                        // Se estiver tudo ok abre o formulário com a comunicação com a página pelo signalr 
                        // Somente quando é chamado pelo site abre 
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1(parEntrada));


                    }
                    else
                    parEntrada = JsonConvert
                                .DeserializeObject<ParametrosEntrada>(args[1]);

                   
                }
                   

                ////o primeiro parametro sempre é o caminho da aplicacao
                parEntrada.Caminho = args[0];

                if (args.Count() <= 1 && !Ferramentas.VerificarProtocolo(parEntrada.Caminho, nomeAplicacao))
                    Ferramentas.Registrar();

                if (parEntrada.Registrar)
                {

                    Ferramentas.RegistrarProtocolo(parEntrada.Caminho, nomeAplicacao);
                    MessageBox.Show("Protocolo registrado com sucesso", nomeAplicacao, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                    return;
                }


                //Verifica se a aplicação já está em execução se sim fecha.
           
                if (AplicacaoAberta())
                {
                    Application.Exit();
                    return;
                }


             

            }
            catch (Exception err)
            {

                MessageBox.Show($"Erro {err}", "Mensage Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
