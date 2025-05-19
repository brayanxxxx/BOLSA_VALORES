using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLSA_VALORES.Models
{
    public class Accion
    {
        public int AccionID { get; set; }
        public string Simbolo { get; set; }
        public string Nombre { get; set; }
        public string Sector { get; set; }
        public decimal PrecioActual { get; set; }
        public decimal VariacionDiaria { get; set; }
    }
}



