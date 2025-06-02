using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLSA_VALORES.Models
{
    public class PortafolioItem
    {
        public string Simbolo { get; set; }
        public string NombreAccion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioActual { get; set; }
        public decimal ValorInvertido { get; set; }
        public decimal ValorActual { get; set; }
        public decimal Ganancia { get; set; }
    }
}
