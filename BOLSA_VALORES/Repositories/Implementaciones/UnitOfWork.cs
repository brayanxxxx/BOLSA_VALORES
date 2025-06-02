using System;
using System.Data.SqlClient;
using BOLSA_VALORES.Repositories.Interfaces;
using BOLSA_VALORES.Repositories.Implementaciones;
using BOLSA_VALORES.Data;
using BOLSA_VALORES.Forms;

namespace BOLSA_VALORES.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        private IAccionRepository _accionRepository;
        private IUsuarioRepository _usuarioRepository;

        private bool disposed = false;

        public UnitOfWork()
        {
            _connection = DatabaseConnection.Instance.GetConnection();
            _transaction = _connection.BeginTransaction();
        }

        public IAccionRepository Acciones
        {
            get
            {
                if (_accionRepository == null)
                    _accionRepository = new AccionRepository(_connection, _transaction);
                return _accionRepository;
            }
        }

        public IUsuarioRepository Usuarios
        {
            get
            {
                if (_usuarioRepository == null)
                    _usuarioRepository = new UsuarioRepository(_connection, _transaction);
                return _usuarioRepository;
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _accionRepository = null;
            _usuarioRepository = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _connection?.Close();
                    _connection?.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
