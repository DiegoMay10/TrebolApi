namespace TrebolAcademy.Models
{
    public class Solicitud
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string identifier { get; set; }
        public int age { get; set; }
        public string afinity { get; set; }
        //public virtual ICollection<Grimorio> Grimorios { get; } = new List<Grimorio>();
        public int idGrimorio { get; set; }
        public string Grimorio { get; set; }
        public int idStatus { get; set; }
        public string status { get; set; }
        public string? title { get; set; }

    }
}
