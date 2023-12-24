
using CoreLayer.Entity;

namespace EntityLayer.Concrete
{
    public class CarImage : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Image { get; set; }
        public Car Car { get; set; }
    }
}
