using AutoMapper;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace GSP.API.Core.Managers.Base
{
    public class BaseManager
    {
        #region Properties
        public readonly IMapper _Mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Claims
        public int _UserId { get; set; }
        public string _UserName { get; set; }
        public string _CurrentHerdCode { get; set; } 
        public string _Role { get; set; }

        #endregion

        #region Constructor
        public BaseManager(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _Mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

            ClaimsIdentity identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null && identity.Claims.Count() > 0)
            {
                _UserId = identity.FindFirst("UserId") != null ? int.Parse(identity.FindFirst("UserId").Value) : 0;
                _UserName = identity.FindFirst("UserName") != null ? identity.FindFirst("UserName").Value : default;
                _CurrentHerdCode = identity.FindFirst("CurrentHerdCode") != null ? identity.FindFirst("CurrentHerdCode").Value : default;
                _Role = identity.FindFirst(ClaimTypes.Role) != null ? identity.FindFirst(ClaimTypes.Role).Value : default;
            }
        }
        #endregion

        #region Validate Model State
        public void ValidateModelState<T>(T model) where T : BaseModel
        {
            if (model?.ValidationResult is null)
                throw new ArgumentNullException("The model was not validated");

            if (!model.ValidationResult.IsValid)
            {
                ResultModel resultModel = new ResultModel();

                foreach (var error in model.ValidationResult.Errors)
                    resultModel.SetError(new Models.System.ErrorModel(error.ErrorCode, error.ErrorMessage));

                throw new CustomException(resultModel);
            }
        } 
        #endregion
    }
}
