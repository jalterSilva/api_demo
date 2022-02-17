using Newtonsoft.Json;

namespace GSP.API.Core.Models.System
{
    public class ErrorModel
    {
        #region Properties
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructor
        [JsonConstructor]
        public ErrorModel(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public ErrorModel(string errorMessage)
        {
            ErrorCode = string.Empty;
            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
