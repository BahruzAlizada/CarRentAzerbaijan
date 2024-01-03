

namespace EntityLayer.Concrete
{
    public class CarModel
    {
        public int Id { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}
