using CoreLayer.Entity;

namespace EntityLayer.Concrete
{
    public class Model : IEntity
    {
        public int Id { get; set; }
        public int MarkaId { get; set; }

        public string Name { get; set; }
        public bool IsDeactive { get; set; }

        public Marka Marka { get; set; }
        public List<Car> Cars { get; set; }
    }
}
