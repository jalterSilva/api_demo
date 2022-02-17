using GSP.API.Core.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GSP.API.Infrastructure.DBSession
{
    public sealed class DbSessionAPIExternal : IDisposable
    {
        #region Properties
        private readonly IWebHostEnvironment _environment;
        public readonly ConnectionString _connectionString;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        #endregion

        #region Constructor
        public DbSessionAPIExternal(IWebHostEnvironment environment, IOptions<ConnectionString> connectionString)
        {
            _environment = environment;
            _connectionString = connectionString.Value;
            Connection = new SqlConnection(GetConnectionString());
            Connection.Open();
        }
        #endregion

        #region Dispose
        public void Dispose() => Connection?.Dispose();
        #endregion

        #region Get Connection
        public string GetConnectionString()
        {
            string connectionString = string.Empty;

            try
            {
                connectionString = _connectionString.UserSaiyan;

                return connectionString;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
