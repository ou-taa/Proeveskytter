using System.ComponentModel.DataAnnotations;

namespace Proeveskytter.Models
{
    public enum IdType
    {
        [Display(Name = "Pas")]
        Pas = 1,

        [Display(Name = "Kørekort")]
        Koerekort = 2,
    }
}
