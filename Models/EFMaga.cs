namespace StolAPI.Models
{
    public class EFMaga:EFBaseModel
    {
/*
        public EFDoctor() { }*/

        public string Name { get; set; }
        public string Description { get; set; }
        public int StartDateWork { get; set; }
        public List<EFStols> EFStols{ get; set; } = new List<EFStols>();
    }
}
