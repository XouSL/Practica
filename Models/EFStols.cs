namespace StolAPI.Models
{
    public class EFStols:EFBaseModel
    {
/*
        public EFPatient() { }*/
        public string Name { get; set; }
       
        public string? Address { get; set; }

        public List<EFMaga> Magas { get; set; } = new List<EFMaga>();

    }
}
