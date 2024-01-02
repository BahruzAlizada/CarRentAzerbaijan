using CoreLayer.Entity;

namespace EntityLayer.Concrete
{
    public class CarModels : IEntity 
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
    }
}
