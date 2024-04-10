using StolAPI.Models;

namespace StolAPI.Replicates
{
    public class Stols
    {

        public EFStols Context { get; set; }
        public Stols(EFStols context) { Context = context; }

        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public string? Address { get => Context.Address; set => Context.Address = value; }

        public List<Maga> Magas { get => Context.Magas.Select(it => new Maga(it)).ToList(); }
    }
}
