using System;
using System.Collections.Generic;

namespace Utilil.Models
{
    public class ParametrosEntrada
    {

        public Guid Id { get; set; }

        public string IdSignalR { get; set; }

        public List<string> IdsClientSignalR { get; set; }

        public bool Registrar { get; set; }

        public string Caminho { get; set; }

        public string Cor { get; set; }

        public ParametrosEntrada()
        {
            Id = Guid.NewGuid();
            IdsClientSignalR = new List<string>();
        }

       

    }
}
