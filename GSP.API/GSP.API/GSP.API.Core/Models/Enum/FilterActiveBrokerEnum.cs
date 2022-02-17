using System.ComponentModel.DataAnnotations;

namespace GSP.API.Core.Models.Enum
{
    public enum FilterActiveBrokerEnum
    {
        [Display(Name = "Todos")]
        Todos,
        [Display(Name = "Ativo")]
        Ativo,
        [Display(Name = "Inativo")]
        Inativo
    }
}
