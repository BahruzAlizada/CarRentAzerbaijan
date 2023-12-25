using CoreLayer.Entity;

namespace EntityLayer.Concrete
{
    public class Model : IEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string? Image { get; set; }
        public bool IsMain { get; set; }
        public List<Model> Children { get; set; }
        public Model Parent { get; set; }
        public int? ParentId { get; set; }
        public bool IsDeactive { get; set; }

        public List<Car> Cars { get; set; }
    }
}
