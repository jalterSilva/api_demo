using Dapper;
using GSP.API.Core.Helpers;
using GSP.API.Core.Models.Broker;
using GSP.API.Core.Models.Enum;
using GSP.API.Core.Models.User;
using GSP.API.Core.Providers.Broker;
using GSP.API.Infrastructure.Base;
using GSP.API.Infrastructure.DBSession;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP.API.Infrastructure.Broker
{
    public class BrokerDAL : BaseDAL, IBrokerDAL
    {
        public BrokerDAL(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> appSettings,
            DbSessionAPI mySqlSession,
            DbSessionAPIExternal sqlServerSession)
            : base(configuration, httpContextAccessor, appSettings, mySqlSession, sqlServerSession)
        {
        }

        #region CreateNewUser
        public async Task<BrokerModel> CreateNewBroker(BrokerModel broker)
        {
            ValidateModelState(broker);

            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" INSERT INTO Broker(CNPJ, BrokerName, Active) ");
            _sqlBuilder.AppendLine(" OUTPUT INSERTED.Id ");
            _sqlBuilder.AppendLine(" VALUES(@CNPJ, @BrokerName, @Active) ");

            _dynamicParameters.Add("@CNPJ", broker.CNPJ, DbType.String, ParameterDirection.Input, 14);
            _dynamicParameters.Add("@BrokerName", broker.BrokerName, DbType.String, ParameterDirection.Input, 50);
            _dynamicParameters.Add("@Active", broker.Active, DbType.String, ParameterDirection.Input);


            broker.SetId(await _apiSession.Connection.ExecuteScalarAsync<int>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction));
            return broker;
        }
        #endregion

        #region Update Broker
        public async Task<BrokerModel> UpdateBroker(BrokerModel broker)
        {
            ValidateModelState(broker);

            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" UPDATE Broker ");
            _sqlBuilder.AppendLine(" SET ");
            _sqlBuilder.AppendLine(" CNPJ = @CNPJ, ");
            _sqlBuilder.AppendLine(" BrokerName  = @BrokerName, ");
            _sqlBuilder.AppendLine(" Active  = @Active ");
            _sqlBuilder.AppendLine(" WHERE Id = @Id; ");

            _dynamicParameters.Add("@CNPJ", broker.CNPJ, DbType.String, ParameterDirection.Input, 14);
            _dynamicParameters.Add("@BrokerName", broker.BrokerName, DbType.String, ParameterDirection.Input, 50);
            _dynamicParameters.Add("@Active", broker.Active, DbType.String, ParameterDirection.Input);
            _dynamicParameters.Add("@Id", broker.Id, DbType.Int32, ParameterDirection.Input);

            await _apiSession.Connection.ExecuteScalarAsync<int>(_sqlBuilder.ToString(), _dynamicParameters, _apiSession.Transaction);
            return broker;
        }
        #endregion

        #region GetBrokerByCNPJ
        public async Task<BrokerModel> GetBrokerByCNPJ(string cnpj)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT TOP 1 Id, CNPJ, BrokerName, Active, CreatedDateTime, LastModifiedDateTime ");
            _sqlBuilder.AppendLine("    FROM Broker ");
            _sqlBuilder.AppendLine(" WHERE CNPJ = @CNPJ ");

            _dynamicParameters.Add("@CNPJ", cnpj, DbType.String, ParameterDirection.Input, 14);

            return await _apiSession.Connection.QueryFirstOrDefaultAsync<BrokerModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);

        }
        #endregion

        #region Get Broker By Id
        public async Task<BrokerModel> GetBrokerById(int id)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT TOP 1 Id, CNPJ, BrokerName, Active ");
            _sqlBuilder.AppendLine("    FROM Broker ");
            _sqlBuilder.AppendLine(" WHERE Id = @Id ");

            _dynamicParameters.Add("@Id", id, DbType.String, ParameterDirection.Input);

            return await _apiSession.Connection.QueryFirstOrDefaultAsync<BrokerModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);

        }
        #endregion

        #region Get All Broker
        public async Task<IEnumerable<BrokerResponseModel>> GetAllBroker(string cnpj, string name, FilterActiveBrokerEnum filterActive)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT  Id ");
            _sqlBuilder.AppendLine("        ,CNPJ ");
            _sqlBuilder.AppendLine(" 	    ,BrokerName ");
            _sqlBuilder.AppendLine(" 	    ,Active ");
            _sqlBuilder.AppendLine(" 	    ,CreatedDateTime ");
            _sqlBuilder.AppendLine(" 	    ,LastModifiedDateTime ");
            _sqlBuilder.AppendLine(" FROM Broker ");
            _sqlBuilder.AppendLine(" WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(cnpj))
            {
                _sqlBuilder.AppendLine(" AND CNPJ = @CNPJ ");
                _dynamicParameters.Add("@CNPJ", cnpj, DbType.String, ParameterDirection.Input, 14);
            }

            if (!string.IsNullOrEmpty(name))
            {
                _sqlBuilder.AppendLine(" AND BrokerName = @BrokerName ");
                _dynamicParameters.Add("@BrokerName", name, DbType.String, ParameterDirection.Input, 50);
            }

            if (filterActive.Equals(FilterActiveBrokerEnum.Ativo))
                _sqlBuilder.AppendLine(" AND Active = 1 ");
            else if (filterActive.Equals(FilterActiveBrokerEnum.Inativo))
                _sqlBuilder.AppendLine(" AND Active = 0 ");


            _sqlBuilder.AppendLine(" ORDER BY Id DESC ");

            return (await _apiSession.Connection.QueryAsync<BrokerResponseModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction)).ToList();

        }
        #endregion

    }
}
