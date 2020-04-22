namespace Models
{
    public class WellModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int CompanyId { get; set; }
        
        public int WorkshopId { get; set; }
        
        public int FieldId { get; set; }

        public CompanyModel Company { get; set; }

        public WorkshopModel Workshop { get; set; }

        public FieldModel Field { get; set; }
    }
}
