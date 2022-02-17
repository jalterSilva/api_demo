using GSP.API.Core.Models.Broker;
using GSP.API.Core.Models.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSP.API.Core.Providers.Broker
{
    public interface IBrokerDAL
    {
        /// <summary>
        /// Create a new Broker
        /// </summary>
        /// <param name="broker"></param>
        /// <returns></returns>
        Task<BrokerModel> CreateNewBroker(BrokerModel broker);

        /// <summary>
        /// Update Broker
        /// </summary>
        /// <param name="broker"></param>
        /// <returns></returns>
        Task<BrokerModel> UpdateBroker(BrokerModel broker);

        /// <summary>
        /// Get broker by CNPJ
        /// </summary>
        /// <param name="CNPJ"></param>
        /// <returns></returns>
        Task<BrokerModel> GetBrokerByCNPJ(string cnpj);

        /// <summary>
        /// Get broker by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BrokerModel> GetBrokerById(int id);
        
        /// <summary>
        /// Get All Broker
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="name"></param>
        /// <param name="filterActive"></param>
        /// <returns></returns>
        Task<IEnumerable<BrokerResponseModel>> GetAllBroker(string cnpj, string name, FilterActiveBrokerEnum filterActive);
    }
}
