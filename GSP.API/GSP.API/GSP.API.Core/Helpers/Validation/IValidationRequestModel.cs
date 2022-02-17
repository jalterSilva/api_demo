using GSP.API.Core.Models.System;

namespace GSP.API.Core.Helpers.Validation
{
    public interface IValidationRequestModel
    {
        ErrorModel GetError();
    }
}
