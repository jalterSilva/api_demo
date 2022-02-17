using System;

namespace GSP.API.Core.Models.Application
{
    public class CustomException : Exception
    {
        #region Properties
        public ResultModel ResultModel { get; private set; }
        #endregion

        #region Constructor
        public CustomException(ResultModel resultModel)
        {
            ResultModel = resultModel;
        }
        #endregion

        #region Methods
        public ResultModel GetError() => ResultModel;
        #endregion
    }
}
