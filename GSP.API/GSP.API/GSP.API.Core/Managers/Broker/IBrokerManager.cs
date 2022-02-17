using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Broker;
using GSP.API.Core.Models.Enum;
using System.Threading.Tasks;

namespace GSP.API.Core.Managers.Broker
{
    public interface IBrokerManager
    {
        /// <summary>
        /// Create a new broker
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> CreateNewBroker(AddOrUpdateBrokerRequestModel requestModel);

        /// <summary>
        /// Update Broker
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateBroker(AddOrUpdateBrokerRequestModel requestModel);

        /// <summary>
        /// Change Broker Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<ResultModel> ChangeBrokerStatus(int id, bool status);

        /// <summary>
        /// Get All Broker
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="name"></param>
        /// <param name="filterActive"></param>
        /// <returns></returns>
        Task<ResultModel> GetAllBroker(string cnpj, string name, FilterActiveBrokerEnum filterActive);

        /// <summary>
        /// Get Broker By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> GetBrokerById(int id);
    }
}
