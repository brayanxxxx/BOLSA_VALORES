using System;
using System.Collections.Generic;
using BOLSA_VALORES.Models;

namespace BOLSA_VALORES.Repositories.Interfaces
{
    public interface IAccionRepository
    {
        List<Accion> ObtenerTodas();
        void ActualizarPreciosConSP();
        void AgregarAccion(Accion accion);
        void ActualizarAccion(Accion accion);
        void EliminarAccion(int accionId);
        void SimularCambios(Action<string> notificarCambioImportante = null);
    }
}
