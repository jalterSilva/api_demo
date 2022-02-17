using AutoMapper;
using GSP.API.Core.Managers.Base;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Broker;
using GSP.API.Core.Models.Enum;
using GSP.API.Core.Models.System;
using GSP.API.Core.Providers.Broker;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GSP.API.Core.Managers.Broker
{
    public class BrokerManager : BaseManager, IBrokerManager
    {

        #region Properties
        private readonly IBrokerDAL _brokerDAL;
        #endregion

        #region Constructor
        public BrokerManager(IBrokerDAL brokerDAL,
                             IMapper mapper,
                             IHttpContextAccessor httpContextAccessor)
                             : base(mapper, httpContextAccessor)
        {
            _brokerDAL = brokerDAL;
        }
        #endregion

        #region Create New Broker
        public async Task<ResultModel> CreateNewBroker(AddOrUpdateBrokerRequestModel requestModel)
        {
            var broker = await _brokerDAL.GetBrokerByCNPJ(requestModel.CNPJ);
            if (broker != null)
                return new ResultModel(new ErrorModel("", "Already exists a broker with this CNPJ."));

            // make the mapper
            var newBroker = _Mapper.Map<BrokerModel>(requestModel);
            ValidateModelState(newBroker); // Validate new broker

            // if ok Insert a new broker
            var insertedBroker = await _brokerDAL.CreateNewBroker(newBroker);

            return new ResultModel(insertedBroker);

        }
        #endregion

        #region Update Broker
        public async Task<ResultModel> UpdateBroker(AddOrUpdateBrokerRequestModel requestModel)
        {
            if (requestModel.Id <= 0 || string.IsNullOrEmpty(requestModel.CNPJ))
                return new ResultModel(new ErrorModel("", "Broker Not found"));

            var broker = await _brokerDAL.GetBrokerById(requestModel.Id);

            if(broker == null)
                return new ResultModel(new ErrorModel("", "Broker Not found"));

            broker.SetCNPJ(requestModel.CNPJ);
            broker.SetBrokerName(requestModel.BrokerName);
            broker.SetStatus(requestModel.Active);

            ValidateModelState(broker);

           var updatedBroker = await _brokerDAL.UpdateBroker(broker);

            return new ResultModel(updatedBroker);

        }
        #endregion

        #region Change Broker Status
        public async Task<ResultModel> ChangeBrokerStatus(int id, bool status)
        {
            if (id <= 0)
                return new ResultModel(new ErrorModel("Corretora não encontrada"));

            var broker = await _brokerDAL.GetBrokerById(id);

            if (broker == null)
                return new ResultModel(new ErrorModel("Corretora não encontrada"));

            if (status)
                broker.SetActive();
            else
                broker.SetInactive();

            await _brokerDAL.UpdateBroker(broker);

            return new ResultModel(true);
        }
        #endregion
       
        #region Get All Broker
        public async Task<ResultModel> GetAllBroker(string cnpj, string name, FilterActiveBrokerEnum filterActive)
        {
            var brokers = await _brokerDAL.GetAllBroker(cnpj, name, filterActive);

            return new ResultModel(brokers);
        }
        #endregion

        #region Get Broker By Id
        public async Task<ResultModel> GetBrokerById(int id)
        {
            if (id <= 0)
                return new ResultModel(new ErrorModel("Corretora não encontrada"));

            var broker = await _brokerDAL.GetBrokerById(id);

            return new ResultModel(broker);
        }
        #endregion
        
    }
}
