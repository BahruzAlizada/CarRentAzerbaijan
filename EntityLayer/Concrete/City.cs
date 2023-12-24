using CoreLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class City : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Name { get; set; }
        public bool IsDeactive { get; set; }

        public List<Car> Cars { get; set; }
    }
}
