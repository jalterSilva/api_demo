using GSP.API.Core.Helpers.ExtensionMethods;
using GSP.API.Core.Models.System;
using System.Collections.Generic;
using System.Linq;

namespace GSP.API.Core.Models.Application
{
    public class ResultModel
    {
        #region Properties
        public bool Success => HasErrors();
        public object ResultData { get; set; }
        private List<ErrorModel> _errors;
        public IReadOnlyCollection<ErrorModel> Errors => _errors?.AsReadOnly();
        #endregion

        #region Constructor
        public ResultModel()
        {
        }

        public ResultModel(IEnumerable<ErrorModel> errorModel)
        {
            SetErrors(errorModel);
        }

        public ResultModel(object resultData)
        {
            SetSucces(resultData);
        }

        public ResultModel(ErrorModel error)
        {
            SetError(error);
        }

        #endregion

        #region Methods
        public void SetSucces(object resultData)
        {
            ResultData = resultData;
            _errors = null;
        }

        public void SetErrors(IEnumerable<ErrorModel> errors)
        {
            ResultData = null;
            _errors = errors.ToList();
        }

        public void SetError(ErrorModel error)
        {
            _errors = _errors ?? new List<ErrorModel>();
            _errors.Add(error);
        }

        public bool HasErrors()
        {
            return !_errors.HasValue();
        }
        #endregion
    }
}