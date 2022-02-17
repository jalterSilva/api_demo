using System.ComponentModel.DataAnnotations;

namespace GSP.API.Core.Models.Enum
{
    public enum OperationStatusEnum
    {
        [Display(Name = "ABERTA")]
        ABERTA = 1,
        [Display(Name = "PARCIAL")]
        PARCIAL = 2,
        [Display(Name = "FINALIZADA")]
        FINALIZADA = 3,
    }
}
