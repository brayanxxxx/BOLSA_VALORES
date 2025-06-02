using System;

namespace BOLSA_VALORES.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccionRepository Acciones { get; }
        IUsuarioRepository Usuarios { get; }

        void Commit();
    }
}
