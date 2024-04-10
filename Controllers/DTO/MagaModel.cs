using StolAPI.Context;
using StolAPI.Replicates;

namespace StolAPI.Controllers.DTO
{
    public class MagaModel
    {
        public MagaModel() { }
        public MagaModel(Maga context)
        {
            id = context.Id;
            name = context.Name;
            description = context.Description;
            startDateWork = context.StartDateWork;
            Stols = context.Stols.Select(it => new StolsModel(it)).ToArray();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int startDateWork { get; set; }
        public StolsModel[] Stols { get; set; }
    }
}
