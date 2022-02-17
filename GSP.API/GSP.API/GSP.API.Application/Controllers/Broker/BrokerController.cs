using GSP.API.Core.Managers.Broker;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Broker;
using GSP.API.Core.Models.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GSP.API.Application.Controllers.Broker
{

    [Route("[controller]")]
    [ApiController]
    [Produces(typeof(ResultModel))]
    public class BrokerController : BaseController
    {

        #region Properties
        private readonly IBrokerManager _brokerManager;

        #endregion

        #region Constructor
        public BrokerController([FromServices] IConfiguration configuration,
                                [FromServices] IWebHostEnvironment environment,
                                [FromServices] IHttpContextAccessor httpContextAccessor,
                                IBrokerManager brokerManager

                                )
                                      : base(configuration, environment, httpContextAccessor)
        {
            _brokerManager = brokerManager;
        }
        #endregion

        #region POST - Create a new broker
        /// <summary>
        /// Create a new broker
        /// </summary>
        /// <param name="requestModel">Broker request model</param>
        /// <returns></returns>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = false)]
        [Route("CreateNewBroker")]
        public async Task<IActionResult> CreateNewBroker([FromBody] AddOrUpdateBrokerRequestModel requestModel)
            => Ok(await _brokerManager.CreateNewBroker(requestModel));
        #endregion

        #region PUT - Update Broker
        /// <summary>
        /// Update Broker
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = false)]
        [Route("UpdateBroker")]
        public async Task<IActionResult> UpdateBroker([FromBody] AddOrUpdateBrokerRequestModel requestModel)
           => Ok(await _brokerManager.UpdateBroker(requestModel));
        #endregion

        #region PUT - Change Broker Status
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("ChangeBrokerStatus/{id}/{status}")]
        public async Task<IActionResult> ChangeBrokerStatus([FromRoute] int id, [FromRoute] bool status)
          => Ok(await _brokerManager.ChangeBrokerStatus(id, status));
        #endregion

        #region GET - Get all Broker
        /// <summary>
        /// Get all Broker
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="name"></param>
        /// <param name="filterActive"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllBroker")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> GetAllBroker([FromQuery] string cnpj = null, [FromQuery] string name = null, [FromQuery] FilterActiveBrokerEnum filterActive = FilterActiveBrokerEnum.Todos)
            => Ok(await _brokerManager.GetAllBroker(cnpj, name, filterActive));
        #endregion

        #region GET - Get Broker By Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBrokerById([FromRoute] int id)
        => Ok(await _brokerManager.GetBrokerById(id));
        #endregion
    }
}
