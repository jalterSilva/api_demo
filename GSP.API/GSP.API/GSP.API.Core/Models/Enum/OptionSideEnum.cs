using System.ComponentModel.DataAnnotations;

namespace GSP.API.Core.Models.Enum
{
    public enum OptionSideEnum
    {
        [Display(Name = "COMPRA")]
        COMPRA = 1,
        [Display(Name = "VENDA")]
        VENDA = 2
    }
}
