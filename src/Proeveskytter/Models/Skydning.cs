using System.ComponentModel.DataAnnotations;

namespace Proeveskytter.Models
{
    public class Skydning
    {
        public int Id { get; set; }
        public Skytte? Skytte { get; set; }
        
        [DataType(DataType.Date)]
        public  DateOnly Dato { get; set; }
        public int SkytteId { get; set; }
    }
}
