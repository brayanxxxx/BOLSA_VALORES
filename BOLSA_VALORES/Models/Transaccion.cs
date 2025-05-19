
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLSA_VALORES.Models
{
    public class Transaccion
    {
        public int TransaccionID { get; set; }
        public int UsuarioID { get; set; }
        public int AccionID { get; set; }
        public string TipoTransaccion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
