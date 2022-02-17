using GSP.API.Core.Providers.UnitOfWork;
using GSP.API.Infrastructure.DBSession;

namespace GSP.API.Infrastructure.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly DbSessionAPI _session;
        #endregion

        #region Constructor
        public UnitOfWork(DbSessionAPI session)
        {
            _session = session;
        }
        #endregion

        #region Begin Transaction
        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }
        #endregion

        #region Commit
        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }
        #endregion

        #region Rollback
        public void Rollback()
        {
            _session.Transaction?.Rollback();
            Dispose();
        }
        #endregion

        #region Dispose
        public void Dispose() => _session.Transaction?.Dispose(); 
        #endregion
    }
}
