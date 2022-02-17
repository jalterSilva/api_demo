using System.ComponentModel.DataAnnotations;

namespace GSP.API.Core.Models.Enum
{
    public enum OperationTypeEnum
    {
        [Display(Name = "CALL")]
        CALL = 1,
        [Display(Name = "PUT")]
        PUT = 2,
    }
}
