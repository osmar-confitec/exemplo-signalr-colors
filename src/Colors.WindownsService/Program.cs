using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Colors
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        static void Main()
        {
#if (!DEBUG)

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);

#else
          var serv =   new Service1();
            serv.InicializarServico();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#endif

        }
    }
}
