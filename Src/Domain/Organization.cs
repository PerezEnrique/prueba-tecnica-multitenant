namespace PruebaTecnicaMultitenant.Src.Domain.Entities
{
    public class Organization 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SlugTenant { get; set; }
    }
}