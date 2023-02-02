using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARAALVAREZLOPEZ.Models
{
    public class Pedido
    {
        public string CodPed { get; set; }
        public string CodCli { get; set; }
        public DateTime Fecha { get; set; }
        public string Envio { get; set; }
        public int Importe { get; set; }
    }
}
