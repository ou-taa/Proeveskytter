using System.ComponentModel.DataAnnotations;

namespace Proeveskytter.Models
{
    public class Skytte
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;

        [Display(Name = "Billed-id type")]
        public IdType IdType { get; set; }

        [Display (Name = "Pas eller kørekort nummer")]
        public string IdNr { get; set; } = string.Empty;
        public ICollection<Skydning>? Skydninger { get; set; }
    }
}
