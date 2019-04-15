
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;
using Utilil.Models;
using System.Linq;

namespace Utilil
{
   public class Ferramentas
    {
        /// <summary>
        /// Verifica se o protocolo existe no windonws 
        /// </summary>
        /// <param name="myAppPath"></param>
        /// <param name="nomeaplicacao"></param>
        /// <returns></returns>
        public static bool VerificarProtocolo(string myAppPath, string nomeaplicacao)
        {
            RegistryKey rootLevel = null;
            RegistryKey shellLevel = null;
            RegistryKey open = null;
            RegistryKey command = null;
            string valor = null;
            rootLevel = Registry.ClassesRoot.OpenSubKey(nomeaplicacao);
            if (rootLevel != null && rootLevel.GetSubKeyNames().ToList().Any())
                shellLevel = rootLevel.OpenSubKey(rootLevel.GetSubKeyNames()[0]);
            if (shellLevel != null)
                open = shellLevel.OpenSubKey(shellLevel.GetSubKeyNames()[0]);
            if (open != null)
                command = open.OpenSubKey(open.GetSubKeyNames()[0]);
            if (command!= null)
                valor = ((string)command.GetValue(null)).Replace("%1", "").Trim();

            return rootLevel != null && !string.IsNullOrEmpty(valor) && valor.Equals(myAppPath);

        }


        /// <summary>
        /// marca no registro do windowns a url e protocolo que será executado. com o nome da aplicação que vem do app.Config
        /// </summary>
        /// <param name="myAppPath"></param>
        public static void RegistrarProtocolo(string myAppPath, string nomeaplicacao)
        {

            RegistryKey key = Registry.ClassesRoot.OpenSubKey(nomeaplicacao, true);

            if (key != null)
            {
                Registry.ClassesRoot.DeleteSubKeyTree(nomeaplicacao);
                key.Close();
            }
            RegistryKey newKey = Registry.ClassesRoot.CreateSubKey(nomeaplicacao);
            newKey.SetValue(string.Empty, "URL:" + nomeaplicacao + " Protocol");
            newKey.SetValue("URL Protocol", string.Empty);
            newKey = newKey.CreateSubKey(@"shell\open\command");
            newKey.SetValue(string.Empty, myAppPath + " " + "%1");
            newKey.Close();

        }


        /// <summary>
        /// Abre novamente o programa com permissão de administrador
        /// </summary>
        /// <returns></returns>
        public static void Registrar()
        {

            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            WindowsPrincipal wp = new WindowsPrincipal(wi);
            bool isAdministrator = wp.IsInRole(WindowsBuiltInRole.Administrator);
            if (!isAdministrator)
            {

                var par = new ParametrosEntrada();
                par.Registrar = true;

                ProcessStartInfo proc = new ProcessStartInfo();
                proc.Arguments = "{\"Registrar\":\"true\"}";
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Verb = "runas";
                try
                {
                    Process.Start(proc);
                    Application.Exit();
                    return;
                }
                catch
                {

                }
            }

        }
    }
}
