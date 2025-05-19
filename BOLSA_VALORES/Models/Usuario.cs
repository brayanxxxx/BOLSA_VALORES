using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLSA_VALORES.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string TipoUsuario { get; set; } // para admin o inversorr
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Saldo { get; set; }
    }
}
