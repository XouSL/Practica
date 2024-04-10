using StolAPI.Replicates;

namespace StolAPI.Controllers.DTO
{
    public class StolsModel
    {
        public StolsModel() { }
        public StolsModel(Stols context)
        {
            id = context.Id;
            name = context.Name;
          
            address = context.Address;
        }

        public int id { get; set; }
        public string name { get; set; }
       
        public string? address { get; set; }
    }
}
