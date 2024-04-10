using StolAPI.Models;

namespace StolAPI.Replicates
{
    public class Maga
    {
        private EFMaga Context { get; set; }
        public Maga(EFMaga context) { Context = context; }
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }

        public string Description { get => Context.Description; set => Context.Description = value; }
        public int StartDateWork { get => Context.StartDateWork; set => Context.StartDateWork = value; }

        public List<Stols> Stols{  get => Context.EFStols.Select(it => new Stols(it)).ToList();
        }
    }
}
