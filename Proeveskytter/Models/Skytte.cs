namespace Proeveskytter.Models
{
    public class Skytte
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public IdType IdType { get; set; }
        public string IdNr { get; set; } = string.Empty;
        public ICollection<Skydning>? Skydninger { get; set; }
    }
}
