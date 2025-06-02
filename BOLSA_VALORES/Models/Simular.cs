using System;
using System.Collections.Generic;
using BOLSA_VALORES.Models;

namespace BOLSA_VALORES.Services
{
    public static class Simular
    {
        private static Random rnd = new Random();

      
        public static void CambioPrecios(List<Accion> acciones)
        {
            foreach (var accion in acciones)
            {
               
                decimal porcentajeCambio = (decimal)(rnd.NextDouble() * 0.10 - 0.05); 

               
                decimal nuevoPrecio = accion.PrecioActual * (1 + porcentajeCambio);

               
                nuevoPrecio = Math.Max(0.01m, Math.Round(nuevoPrecio, 2));

              
                accion.VariacionDiaria = porcentajeCambio * 100; 
                accion.PrecioActual = nuevoPrecio;
            }
        }
    }
}
