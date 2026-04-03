namespace Proeveskytter.Models
{
    public class Skydning
    {
        public int Id { get; set; }
        public Skytte Skytte { get; set; } = new Skytte();
        public  DateOnly Dato { get; set; }
        public int SkytteId { get; set; }
    }
}
