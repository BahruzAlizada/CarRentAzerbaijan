using CoreLayer.Entity;

namespace EntityLayer.Concrete
{
    public class Marka : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsDeactive { get; set; }

        public List<Model> Models { get; set; }
        public List<Car> Cars { get; set; }
    }
}
